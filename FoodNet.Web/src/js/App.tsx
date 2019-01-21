import * as React from "react";
import { Switch, Route } from "react-router-dom";
import * as routes from "./Routes";
import { LoginPage } from "./Components/LoginPage";
import { SignUpPage } from "./Components/SignUpPage";
import { FridgePage } from "./Components/FridgePage";
import { RecipesPage } from "./Components/RecipesPage";
import { NotFoundPage } from "./Components/NotFoundPage";


export class App extends React.Component<{}, {}> {
    constructor(props: any) {
        super(props);

    }

    render() {
        return (<div>
            {
                <Switch>
                    <Route exact path={routes.LoginPage} component={LoginPage} />
                    <Route exact path={routes.SignUpPage} component={SignUpPage} />
                    <Route exact path={routes.FridgePage} component={FridgePage} />
                    <Route exact path={routes.RecipesPage} render={() => <RecipesPage isInResultMode={false} />} />
                    <Route exact path={routes.ResultRecipesPage} render={() => <RecipesPage isInResultMode={true} />} />
                    <Route path="*" component={NotFoundPage} />
                </Switch>
            }
        </div>
        );
    }
}
