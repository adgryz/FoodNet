import * as React from "react";
import { Card, CardHeader, CardActions, CardText, TextField, RadioButton, Checkbox, RaisedButton, FloatingActionButton, Chip, AutoComplete, Dialog } from "material-ui";
import { ContentAddCircle, ContentAdd } from "material-ui/svg-icons";
import * as Router from "react-router-dom";
import * as routes from "../Routes";
import axios from "axios";
import { Link } from "react-router-dom";
import { Recipe, Product, Tag, RecipeUpdate } from "../Conracts";
import { NewProductDialog } from "./NewProductDialog";
import { httpClient } from "../Infrastructure/httpsClient";
import { generate } from "uuidjs";

export interface RecipeCreatorProps {
    readonly onCancel: () => void;
    readonly refetchRecipes: () => void;
    readonly isInEdition: boolean;
    readonly editedRecipe?: Recipe;
}

interface RecipeCreatorState {
    readonly actualRecipe: Recipe;
    readonly products: Product[];
    readonly productsSearchText: string;
    readonly tags: Tag[];
    readonly tagsSearchText: string;
    readonly isNewProductOpen: boolean;
}


export class RecipeCreator extends React.Component<RecipeCreatorProps, RecipeCreatorState> {
    public constructor(props: RecipeCreatorProps) {
        super(props);
        let emptyRecipe: Recipe;
        emptyRecipe = { id: generate(), title: "", description: "", products: [], tags: [], isPrivate: false, isMy: true };
        let recipe = this.props.isInEdition ? this.props.editedRecipe as Recipe : emptyRecipe;
        this.state = {
            actualRecipe: recipe,
            products: [],
            productsSearchText: "",
            tags: [],
            tagsSearchText: "",
            isNewProductOpen: false,
        }
    }

    render() {
        return (
            <div>
                <div style={{ display: "flex", flexFlow: "column", marginLeft: "50px", alignItems: "center" }}>
                    <div style={{ display: "flex", justifyContent: "space-between", width: "850px" }}>
                        <div style={{ display: "flex" }}>
                            {this.state.actualRecipe.tags.map(tag =>
                                <Chip
                                    labelColor="#fff"
                                    onRequestDelete={() => this.deleteTag(tag.id)}
                                    labelStyle={{ fontWeight: "bold" }}
                                    style={{ marginRight: "5px", height: "30px" }}>
                                    {tag.text}
                                </Chip>)}
                            <AutoComplete
                                filter={AutoComplete.caseInsensitiveFilter}
                                dataSource={this.state.tags.map(tag => tag.text)}
                                searchText={this.state.tagsSearchText}
                                onUpdateInput={(text) => this.setState({ tagsSearchText: text })}
                                onNewRequest={this.addTag} />
                        </div>
                        <Checkbox
                            style={{ fontSize: "18px", float: "right", display: "flex", alignItems: "center", color: "rgb(224,224,224)", width: "150px" }}
                            label="Is private"
                            checked={this.state.actualRecipe.isPrivate}
                            onCheck={(event: object, isChecked: boolean) => this.setState({ actualRecipe: { ...this.state.actualRecipe, isPrivate: isChecked } })}
                        />
                    </div>

                    <Card style={{ width: "850px", height: "600px", marginTop: "25px" }}>
                        <CardText>
                            <TextField
                                floatingLabelText="Title"
                                defaultValue={this.state.actualRecipe.title}
                                onChange={(_, title) => this.setState({ actualRecipe: { ...this.state.actualRecipe, title: title } })}
                            />
                            <div style={{ display: "flex" }}>
                                <div style={{ display: "flex", marginTop: "15px", width: "30%", flexFlow: "column" }}>
                                    <div>PRODUCTS :</div>
                                    {this.state.actualRecipe.products.map(product =>
                                        <Chip
                                            labelColor="#fff"
                                            labelStyle={{ fontWeight: "bold" }}
                                            onRequestDelete={() => this.deleteProduct(product.id)}
                                            style={{ margin: "5px", width: "150px", display: "flex", justifyContent: "center" }}>
                                            {product.name}
                                        </Chip>
                                    )}
                                    <AutoComplete
                                        filter={AutoComplete.caseInsensitiveFilter}
                                        dataSource={this.state.products.map(product => product.name)}
                                        searchText={this.state.productsSearchText}
                                        onUpdateInput={(text) => this.setState({ productsSearchText: text })}
                                        onNewRequest={this.addProduct} />
                                    <RaisedButton
                                        label="New Product"
                                        secondary={true}
                                        style={{ width: "150px", margin: "5px" }}
                                        onClick={() => this.handleNewProductDialog(true)} />
                                </div>

                                <div style={{ marginLeft: "15px", marginRight: "15px", fontSize: "20px", height: "500px", width: "60%" }}>
                                    <TextField
                                        floatingLabelText="Description"
                                        multiLine={true}
                                        fullWidth={true}
                                        rows={18}
                                        rowsMax={18}
                                        defaultValue={this.state.actualRecipe.description}
                                        onChange={(_, desc) => this.setState({ actualRecipe: { ...this.state.actualRecipe, description: desc } })}
                                    />
                                </div>
                            </div>
                        </CardText>
                    </Card>
                    <div style={{ marginTop: "30px", width: "100%" }}>
                        <RaisedButton
                            label="Cancel"
                            secondary={true}
                            style={{ marginRight: "20px" }}
                            onClick={this.props.onCancel} />
                        <RaisedButton
                            label={this.props.isInEdition ? "Update Recipe" : "Add Recipe"}
                            onClick={this.props.isInEdition ? this.updateRecipe : this.addRecipe}
                            style={{ float: "right" }}
                            secondary={true} />
                    </div>
                </div>
                <Dialog
                    title="Add new product"
                    modal={false}
                    open={this.state.isNewProductOpen}
                    onRequestClose={() => this.handleNewProductDialog(false)}>
                    <NewProductDialog
                        handleNewProductDialog={this.handleNewProductDialog}
                        refetchProducts={() => null}
                    />
                </Dialog>
            </div >

        );
    }

    componentDidMount() {
        let tags = this.fetchTags();
        let products = this.fetchProducts();
    }

    private addRecipe = async () => {
        console.warn(this.state.actualRecipe);
        let newRecipe: RecipeUpdate;
        newRecipe = {
            id: this.state.actualRecipe.id,
            title: this.state.actualRecipe.title,
            description: this.state.actualRecipe.description,
            tags: this.state.actualRecipe.tags,
            products: this.state.actualRecipe.products,
            isPrivate: this.state.actualRecipe.isPrivate
        }
        await httpClient.executeRequest("PUT", "/recipes", newRecipe);
        this.props.refetchRecipes();
        this.props.onCancel();
    }

    private updateRecipe = async () => {
        console.warn(this.state.actualRecipe);
        let updatedRecipe: RecipeUpdate;
        updatedRecipe = {
            id: this.state.actualRecipe.id,
            title: this.state.actualRecipe.title,
            description: this.state.actualRecipe.description,
            tags: this.state.actualRecipe.tags,
            products: this.state.actualRecipe.products,
            isPrivate: this.state.actualRecipe.isPrivate
        }
        await httpClient.executeRequest("POST", "/recipes", updatedRecipe);
        this.props.refetchRecipes();
        this.props.onCancel();
    }

    private addProduct = (chosenProduct: string, index: number) => {
        let newProduct = this.state.products[index];
        let newProductsSet = this.state.actualRecipe.products.slice();
        newProductsSet.push(newProduct);
        this.setState({
            actualRecipe: { ...this.state.actualRecipe, products: newProductsSet },
            productsSearchText: "",
        });
    }

    private deleteProduct = (productId: string) => {
        let newProducts: Product[];
        newProducts = [];
        this.state.actualRecipe.products.forEach(product => {
            if (product.id !== productId) {
                newProducts.push(product);
            }
        });
        this.setState({
            actualRecipe: { ...this.state.actualRecipe, products: newProducts }
        })
    }

    private addTag = (chosenTag: string, index: number) => {
        let newTag = this.state.tags[index];
        let newTagsSet = this.state.actualRecipe.tags.slice();
        newTagsSet.push(newTag);
        this.setState({
            actualRecipe: { ...this.state.actualRecipe, tags: newTagsSet },
            tagsSearchText: "",
        });
    }

    private deleteTag = (tagId: string) => {
        let newTags: Tag[];
        newTags = [];
        this.state.actualRecipe.tags.forEach(tag => {
            if (tag.id !== tagId) {
                newTags.push(tag);
            }
        });
        this.setState({
            actualRecipe: { ...this.state.actualRecipe, tags: newTags }
        })
    }

    private fetchTags = async () => {
        let tags = await httpClient.executeRequest("GET", "/tags/all", {});

        this.setState({ tags: tags });
    }

    private fetchProducts = async () => {
        let products = await httpClient.executeRequest("GET", "/products", {});
        this.setState({ products: products });
    }

    private handleNewProductDialog = (isOpen: boolean) => this.setState({ isNewProductOpen: isOpen });


}
