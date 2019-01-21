import * as React from "react";
import { Card, CardHeader, CardActions, CardText, TextField, RadioButton, Checkbox, RaisedButton, FloatingActionButton, FlatButton, Chip, Dialog } from "material-ui";
import { ContentAddCircle, ContentAdd } from "material-ui/svg-icons";
import * as Router from "react-router-dom";
import * as routes from "../Routes";
import axios from "axios";
import { ProductCategory, Product } from "../Conracts";
import { Link } from "react-router-dom";
import { NewProductDialog } from "./NewProductDialog";
import { httpClient } from "../Infrastructure/httpsClient";

// GET CATEGORIES
// GET PRODUCTS
// ADD NEW PRODUCT

// GET FRIDGE PRODUCTS
// ADD PRODUCT TO FRIDGE
// REMOVE PRODUCT FROM FRIDGE

export interface FridgePageProps {
}

interface FridgePageState {
    readonly categories: ProductCategory[];
    readonly products: Product[];
    readonly productsFromCategory: Product[];
    readonly selectedCategoryId?: string;
    readonly fridgeProducts: Product[];
    readonly isNewProductOpen: boolean;
}


export class FridgePage extends React.Component<FridgePageProps, FridgePageState> {
    public constructor(props: FridgePageProps) {
        super(props);
        this.state = {
            categories: [],
            products: [],
            productsFromCategory: [],
            fridgeProducts: [],
            isNewProductOpen: false,
        }
    }

    private dialogActions = [

    ];

    render() {
        return (
            <div>
                <Card
                    style={{ width: "1000px", height: "800px", margin: "auto", marginTop: "100px", paddingTop: "100px", position: "relative" }}>
                    <div style={{ display: "flex", position: "relative", paddingRight: "50px", paddingLeft: "50px" }}>
                        <div style={{ display: "flex", justifyContent: "center", flexFlow: "column", alignItems: "center" }}>
                            {this.state.categories.map(category =>
                                <RaisedButton
                                    style={{ width: "200px", margin: "5px" }}
                                    label={category.name}
                                    primary={true}
                                    onClick={() => this.changeCategory(category.id)}
                                />
                            )}
                        </ div>

                        <div style={{ display: "flex", flexFlow: "column", alignItems: "center", marginLeft: "15px" }}>
                            <div style={{ display: "flex", flexFlow: "column", alignItems: "center", maxHeight: "400px", flexWrap: "wrap" }}>
                                {this.state.productsFromCategory.map(product =>
                                    <Checkbox
                                        key={product.id}
                                        checked={this.productIsInFridge(product)}
                                        label={product.name}
                                        onCheck={(event: object, isChecked: boolean) => this.handleProductClick(event, isChecked, product)}
                                        style={{ float: "left", marginBottom: "10px", }} />
                                )}
                            </div>
                            <RaisedButton
                                label="Add product"
                                primary={true}
                                onClick={() => this.handleNewProductDialog(true)} />
                        </ div>

                        <div style={{ display: "flex", justifyContent: "center", flexFlow: "column", alignItems: "center", position: "absolute", right: "50px" }}>
                            <div style={{ fontWeight: "bold" }}>
                                FRIDGE
                            </div>
                            {this.state.fridgeProducts.map(fridgeProduct =>
                                <Chip
                                    onRequestDelete={() => this.RemoveProductFromFridge(fridgeProduct)}
                                    style={{ margin: "5px", width: "160px", display: "flex", justifyContent: "center" }}>
                                    {fridgeProduct.name}
                                </Chip>
                            )}
                        </ div>

                    </div>

                    <div style={{ position: "absolute", bottom: "50px", width: "100%" }}>
                        <Link to={routes.RecipesPage}>
                            <RaisedButton
                                label="Go to all recipies"
                                secondary={true}
                                style={{ marginLeft: "50px" }} />
                        </Link>
                        <Link to={routes.ResultRecipesPage}>
                            <RaisedButton
                                label="Show what i can cook "
                                secondary={true}
                                style={{ float: "right", marginRight: "50px" }} />
                        </Link>
                    </div>
                </Card>
                <Dialog
                    title="Add new product"
                    modal={false}
                    open={this.state.isNewProductOpen}
                    onRequestClose={() => this.handleNewProductDialog(false)}
                >
                    <NewProductDialog
                        handleNewProductDialog={this.handleNewProductDialog}
                        refetchProducts={this.refetchProducts} />
                </Dialog>
            </div>
        );
    }


    // INITIAL DATA FETCH
    componentDidMount() {
        this.fetchData();
    }

    private fetchData = async () => {
        let categories = await this.fetchCategories();
        let products = await this.fetchProducts();
        let firstProducts = this.getProductsFromCategory((categories[0] as ProductCategory).id, products);
        let fridgeProducts = await this.fetchFridgeProducts();
        this.setState({
            categories: categories,
            products: products,
            productsFromCategory: firstProducts,
            selectedCategoryId: categories[0].id,
            fridgeProducts: fridgeProducts,
        });
    }

    private fetchCategories = async () => {
        let categories = await httpClient.executeRequest("GET", "/products/categories", {});
        return categories;
    }

    private fetchProducts = async () => {
        let products = await httpClient.executeRequest("GET", "/products", {});
        return products;
    }

    private refetchProducts = async () => {
        let products = await this.fetchProducts();
        let displayProducts = this.getProductsFromCategory(this.state.selectedCategoryId as string, products);
        this.setState({ products: products, productsFromCategory: displayProducts });
    }

    private getProductsFromCategory = (categoryId: string, products?: Product[]) => {
        if (products === undefined)
            return this.state.products.filter(product => product.categoryId === categoryId);
        else
            return products.filter(product => product.categoryId === categoryId);
    }

    private fetchFridgeProducts = async () => {
        let fridgeProducts = await httpClient.executeRequest("GET", "/fridge/products", {});
        return fridgeProducts;
    }
    // END OF FETCH


    private changeCategory = (categoryId: string) => {
        let actualProducts = this.getProductsFromCategory(categoryId);
        this.setState({
            productsFromCategory: actualProducts,
            selectedCategoryId: categoryId
        })
    }

    // FRIDGE MAINTANANCE
    private productIsInFridge = (product: Product) => -1 !== this.state.fridgeProducts.findIndex(p => p.id == product.id);

    private handleProductClick = (event: object, isInputChecked: boolean, product: Product) => {
        if (isInputChecked)
            this.AddProductToFridge(product);
        else
            this.RemoveProductFromFridge(product);
    }

    private AddProductToFridge = async (product: Product) => {
        await httpClient.executeRequest("PUT", "/fridge", product.id);
        let fridgeProducts = await this.fetchFridgeProducts();
        this.setState({ fridgeProducts: fridgeProducts });
    }

    private RemoveProductFromFridge = async (product: Product) => {
        await httpClient.executeRequest("DELETE", "/fridge", product.id);
        let fridgeProducts2 = await this.fetchFridgeProducts();
        this.setState({ fridgeProducts: fridgeProducts2 });
    }

    // NEW PRODUCT
    private handleNewProductDialog = (isOpen: boolean) => this.setState({ isNewProductOpen: isOpen });

    //
}
