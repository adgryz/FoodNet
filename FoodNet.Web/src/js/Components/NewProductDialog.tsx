import * as React from "react";
import { Card, CardHeader, CardActions, CardText, TextField, RadioButton, Checkbox, RaisedButton, FloatingActionButton, DropDownMenu, MenuItem } from "material-ui";
import { ContentAddCircle, ContentAdd } from "material-ui/svg-icons";
import * as Router from "react-router-dom";
import * as routes from "../Routes";
import axios from "axios";
import { Link } from "react-router-dom";
import { Product, ProductCategory } from "../Conracts";
import { generate } from "uuidjs";
import { httpClient } from "../Infrastructure/httpsClient";

export interface NewProductDialogProps {
    handleNewProductDialog: (isOpen: boolean) => void;
    refetchProducts: () => void;
}

interface NewProductDialogState {
    readonly name: string;
    readonly description: string;
    readonly category?: ProductCategory;
    readonly categories: ProductCategory[];
}

export class NewProductDialog extends React.Component<NewProductDialogProps, NewProductDialogState> {
    public constructor(props: NewProductDialogProps) {
        super(props);
        this.state = {
            name: "",
            description: "",
            categories: []
        }
    }

    render() {
        return (
            <div style={{ display: "flex", flexFlow: "column" }}>
                <TextField
                    value={this.state.name}
                    floatingLabelText="Name"
                    onChange={(_, name) => this.setState({ name })}
                    style={{ margin: "15px" }}
                />
                <TextField
                    value={this.state.description}
                    floatingLabelText="Description"
                    onChange={(_, description) => this.setState({ description })}
                    style={{ margin: "15px" }}

                />
                <div style={{ margin: "15px", display: "flex", alignItems: "center" }}>
                    <div> Category : </div>
                    <DropDownMenu value={this.state.category} onChange={(e, k, category) => this.setState({ category })}>
                        {this.state.categories.map(category =>
                            <MenuItem value={category} primaryText={category.name} />)}
                    </DropDownMenu>
                </div>
                <div style={{ display: "flex", justifyContent: "space-between" }}>
                    <RaisedButton
                        primary={true}
                        label="Cancel"
                        onClick={() => this.props.handleNewProductDialog(false)}
                        style={{ width: "100px" }} />
                    <RaisedButton
                        primary={true}
                        label="Add"
                        onClick={() => { this.addNewProduct(); this.props.handleNewProductDialog(false) }}
                        style={{ width: "100px" }} />
                </div>
            </div >
        );
    }

    componentDidMount() {
        this.fetchCategories();
    }

    private addNewProduct = async () => {
        let newProduct: Product;
        newProduct = {
            id: generate(),
            name: this.state.name,
            description: this.state.description,
            categoryId: (this.state.category as ProductCategory).id,
        }
        await httpClient.executeRequest("PUT", "/products", newProduct);
        this.props.refetchProducts();
    }

    private fetchCategories = async () => {
        let categories = await httpClient.executeRequest("GET", "/products/categories", {});
        this.setState({ categories: categories });
    }

}
