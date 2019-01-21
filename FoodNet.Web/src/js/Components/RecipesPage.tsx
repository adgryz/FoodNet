import * as React from "react";
import { Card, CardHeader, CardActions, CardText, TextField, RadioButton, Checkbox, RaisedButton, FloatingActionButton, Chip, CardTitle, Drawer, Divider } from "material-ui";
import { ContentAddCircle, ContentAdd } from "material-ui/svg-icons";
import * as Router from "react-router-dom";
import * as routes from "../Routes";
import axios from "axios";
import { Recipe, Product, Tag } from "../Conracts";
import { Link } from "react-router-dom";
import { RecipeViewer } from "./RecipeViewer";
import { RecipeCreator } from "./RecipeCreator";
import { httpClient } from "../Infrastructure/httpsClient";
import { blue500, blue800, blue700 } from "material-ui/styles/colors";

export interface RecipesPageProps {
    readonly isInResultMode: boolean;
}

interface RecipesPageState {
    recipes: Recipe[];
    tags: Tag[];
    selectedTags: Tag[];
    selectedRecipe?: Recipe;
    isInCreationMode: boolean;
    isInEditionMode: boolean;
    areFiltersOpen: boolean;
    onlyUserRecipes: boolean;
}

export class RecipesPage extends React.Component<RecipesPageProps, RecipesPageState> {
    public constructor(props: RecipesPageProps) {
        super(props);
        this.state = {
            recipes: [],
            tags: [],
            selectedTags: [],
            areFiltersOpen: false,
            isInCreationMode: false,
            isInEditionMode: false,
            onlyUserRecipes: false,
        }
    }

    render() {
        return (
            <div>
                <Card
                    style={{
                        width: "1200px", height: "800px", margin: "auto",
                        marginTop: "100px", paddingTop: "100px", position: "relative",
                        padding: "50px"
                    }}>
                    <div style={{ display: "flex", width: "100%", height: "800px", margin: "5px" }}>

                        <div style={{ display: "flex", flexFlow: "column" }}>
                            <Link to={routes.FridgePage}>
                                <RaisedButton
                                    label="GO TO FRIDGE"
                                    secondary={true}
                                    style={{ marginBottom: "5px", width: "200px" }} />
                            </Link>
                            <RaisedButton
                                label={this.state.areFiltersOpen ? "HIDE FILTERS" : "SHOW FILTERS"}
                                secondary={true}
                                style={{ marginBottom: "25px", width: "200px" }}
                                onClick={() => this.setState({ areFiltersOpen: !this.state.areFiltersOpen })} />
                            {this.state.recipes.map(recipe =>
                                <RaisedButton
                                    labelColor="#fff"
                                    backgroundColor={recipe.isMy ? blue700 : blue500}
                                    label={recipe.title}
                                    style={{ width: "200px", marginBottom: "5px" }}
                                    onClick={() => this.selectRecipe(recipe)} />)}
                        </div>

                        {this.state.isInCreationMode ?
                            <RecipeCreator
                                isInEdition={this.state.isInEditionMode}
                                editedRecipe={this.state.isInEditionMode ? this.state.selectedRecipe : undefined}
                                onCancel={this.handleCancel}
                                refetchRecipes={this.fetchRecipes} />
                            :
                            <RecipeViewer
                                recipe={this.state.selectedRecipe}
                                onAdd={this.handleAddRecipe}
                                onEdit={this.handleEditRecipe}
                                onRemove={this.handleRemoveRecipe}
                                isInResultMode={this.props.isInResultMode} />}
                    </div>
                </Card>
                <Drawer open={this.state.areFiltersOpen}>
                    <div style={{ display: "flex", flexFlow: "column", padding: "10px" }}>
                        <Checkbox
                            label="Show only my recipes"
                            checked={this.state.onlyUserRecipes}
                            onCheck={(event: object, isChecked: boolean) => this.setState({ onlyUserRecipes: isChecked })}
                            style={{ marginBottom: "10px", marginTop: "10px" }}
                        />
                        <Divider inset={false} />
                        <span style={{ marginTop: "10px" }} >Tags :</span>
                        {
                            this.state.tags.map(tag =>
                                <Checkbox
                                    style={{ marginTop: "10px" }}
                                    label={tag.text}
                                    onCheck={(event: object, isChecked: boolean) => this.handleTagClick(event, isChecked, tag)}
                                    checked={this.tagIsSelected(tag)} />
                            )
                        }
                        <RaisedButton secondary={true} label="APPLY FILTERS" onClick={() => this.fetchRecipes()} style={{ marginTop: "10px" }} />
                    </div>
                </Drawer>
            </div>
        );
    }

    componentWillReceiveProps(props: RecipesPageProps) {
        this.fetchRecipes(props.isInResultMode);
        this.fetchTags();
    }

    componentDidMount() {
        this.fetchRecipes();
        this.fetchTags();
    }

    private handleAddRecipe = () => this.setState({ isInCreationMode: true, isInEditionMode: false })
    private handleEditRecipe = () => this.setState({ isInCreationMode: true, isInEditionMode: true })
    private handleRemoveRecipe = async () => {
        let tags = await httpClient.executeRequest("DELETE", "/recipes", (this.state.selectedRecipe as Recipe).id);
        let recipes = this.fetchRecipes();
    }
    private handleCancel = () => this.setState({ isInCreationMode: false })

    private fetchRecipes = async (isInResultMode?: boolean) => {
        if (isInResultMode === undefined) {
            isInResultMode = this.props.isInResultMode;
        }
        let recipesEndpoint = isInResultMode ? "/results" : "/recipes";
        recipesEndpoint += this.state.onlyUserRecipes ? "" : "/all";

        if (this.state.selectedTags.length > 0) {
            let tagQuery = "";
            this.state.selectedTags.forEach(tag => {
                tagQuery += "&tags=" + tag.id;
            })
            tagQuery = "?" + tagQuery.substring(1);
            recipesEndpoint += tagQuery;
        }

        let recipes = await httpClient.executeRequest("GET", recipesEndpoint, {});
        this.setState({ recipes: recipes, selectedRecipe: recipes[0] })
    }

    private tagIsSelected = (tag: Tag) => -1 !== this.state.selectedTags.findIndex(t => t.id == tag.id);

    private handleTagClick = (event: object, isInputChecked: boolean, tag: Tag) => {
        let newTags = this.state.selectedTags.slice();
        if (isInputChecked) {
            newTags.push(tag);
        } else {
            newTags = [];
            this.state.selectedTags.forEach(t => {
                if (t.id != tag.id) {
                    newTags.push(t);
                }
            });
        }
        this.setState({ selectedTags: newTags });
    }

    private fetchTags = async () => {
        let tags = await httpClient.executeRequest("GET", "/tags/all", {});
        this.setState({ tags: tags });
    }

    private selectRecipe = (recipe: Recipe) => {
        this.setState({ selectedRecipe: recipe });
    }
}
