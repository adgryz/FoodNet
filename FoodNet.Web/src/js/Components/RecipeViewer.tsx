import * as React from "react";
import { Card, CardHeader, CardActions, CardText, TextField, RadioButton, Checkbox, RaisedButton, FloatingActionButton, Chip, CardTitle } from "material-ui";
import { ContentAddCircle, ContentAdd } from "material-ui/svg-icons";
import * as Router from "react-router-dom";
import * as routes from "../Routes";
import axios from "axios";
import { Link } from "react-router-dom";
import { Recipe } from "../Conracts";
import { red900, red600 } from "material-ui/styles/colors";

export interface RecipeViewerProps {
    readonly recipe?: Recipe;
    readonly onAdd: () => void;
    readonly onEdit: () => void;
    readonly onRemove: () => void;
    readonly isInResultMode: boolean;
}

interface RecipeViewerState {

}


export class RecipeViewer extends React.Component<RecipeViewerProps, RecipeViewerState> {
    public constructor(props: RecipeViewerProps) {
        super(props);
        this.state = {

        }
    }

    render() {
        return (
            <div style={{ display: "flex", flexFlow: "column", marginLeft: "50px", alignItems: "center" }}>
                <div style={{ display: "flex", justifyContent: "space-between", width: "850px" }}>
                    <div style={{ display: "flex" }}>
                        {this.props.recipe && this.props.recipe.tags.map(tag =>
                            <Chip
                                labelColor="#fff"
                                labelStyle={{ fontWeight: "bold" }}
                                style={{ marginRight: "5px" }}>
                                {tag.text}
                            </Chip>)}
                    </div>
                    {this.props.recipe && this.props.recipe.isPrivate &&
                        <div style={{ fontSize: "18px", float: "right", display: "flex", alignItems: "center", color: "rgb(224,224,224)" }}>
                            PRIVATE
                                    </div>}
                </div>

                {this.props.recipe &&
                    <Card style={{ width: "850px", height: "600px", marginTop: "25px" }}>
                        <CardTitle
                            style={{ fontWeight: "bold", fontSize: "24px" }}
                            title={this.props.recipe.title}
                        />
                        <CardText>
                            <div style={{ display: "flex" }}>
                                <div>
                                    {this.props.recipe.products.map(product =>
                                        <Chip
                                            labelColor="#fff"
                                            labelStyle={{ fontWeight: "bold" }}
                                            style={{ margin: "5px", width: "150px", display: "flex", justifyContent: "center" }}>
                                            {product.name}
                                        </Chip>
                                    )}
                                </div>

                                <div style={{ marginLeft: "15px", fontSize: "20px", overflowY: "auto", height: "500px", width: "100%" }}>
                                    {this.props.recipe.description}
                                </div>
                            </div>
                        </CardText>
                    </Card>}
                <div style={{ marginTop: "30px", width: "100%" }}>
                    <RaisedButton
                        label="Add new recipe"
                        secondary={true}
                        style={{ marginRight: "20px" }}
                        onClick={this.props.onAdd} />
                    {this.props.recipe && this.props.recipe.isMy &&
                        <RaisedButton
                            label="Edit recipe"
                            onClick={this.props.onEdit}
                            style={{ marginRight: "20px" }}
                            secondary={true} />}
                    {this.props.recipe && this.props.recipe.isMy &&
                        <RaisedButton
                            label="Delete recipe"
                            onClick={this.props.onRemove}
                            labelColor="#fff"
                            backgroundColor={red600} />}
                    <Link to={this.props.isInResultMode ? routes.RecipesPage : routes.ResultRecipesPage}>
                        <RaisedButton
                            label={this.props.isInResultMode ? "All recipies" : "Recipes i can cook"}
                            style={{ float: "right" }}
                            secondary={true} />
                    </Link>
                </div>
            </div>
        );
    }
}
