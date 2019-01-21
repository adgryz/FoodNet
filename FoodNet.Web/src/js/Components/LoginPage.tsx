import * as React from "react";
import { Card, CardHeader, CardActions, CardText, TextField, RadioButton, Checkbox, RaisedButton, FloatingActionButton } from "material-ui";
import { ContentAddCircle, ContentAdd } from "material-ui/svg-icons";
import * as Router from "react-router-dom";
import * as routes from "../Routes";
import axios from "axios";
import { Link } from "react-router-dom";
import cfg from "config";
import { httpClient } from "../Infrastructure/httpsClient";

// SIGNIN

export interface LoginPageProps {
}

interface LoginPageState {
    readonly username: string;
    readonly password: string;
    readonly usernameError: string;
    readonly passwordError: string;
}

type LoginPageWithRouterProps = Router.RouteComponentProps<{}> & LoginPageProps;

export class LoginPage extends React.Component<LoginPageWithRouterProps, LoginPageState> {
    public constructor(props: LoginPageWithRouterProps) {
        super(props);
        this.state = {
            username: "",
            password: "",
            usernameError: "",
            passwordError: "",
        }
    }

    render() {
        return (
            <div style={{ display: "flex", justifyContent: "center" }}>
                <Card
                    style={{ width: "600px", marginTop: "200px" }}>
                    <div style={{ display: "flex", justifyContent: "center", flexFlow: "column", alignItems: "center", padding: "40px" }}>
                        <div style={{ fontSize: "30px", fontWeight: "bold" }}>FoodNET</div>
                        <TextField
                            floatingLabelText="Username"
                            value={this.state.username}
                            onChange={(_, username) => this.setState({ username })}
                            errorText={this.state.usernameError}
                        /><br />
                        <TextField
                            floatingLabelText="Password"
                            type="password"
                            value={this.state.password}
                            onChange={(_, password) => this.setState({ password })}
                            errorText={this.state.passwordError}
                        /><br />
                        <div style={{ display: "flex", width: "50%", justifyContent: "space-around" }}>
                            <RaisedButton
                                primary={true}
                                label="LOGIN"
                                onClick={this.tryLogin}
                            />
                            <Link to={routes.SignUpPage}>
                                <RaisedButton
                                    secondary={true}
                                    label="SIGN UP" />
                            </Link>
                        </div>
                    </div>
                </Card>
            </div>
        );
    }

    private clearError = () => this.setState({ passwordError: "", usernameError: "" });

    private validate = () => {
        if (this.state.username.length < 1) {
            this.setState({ usernameError: "No username" });
            return false;
        }
        if (this.state.password.length < 1) {
            this.setState({ passwordError: "No password" });
            return false;
        }

        return true;
    }
    private tryLogin = async () => {

        this.clearError();
        if (!this.validate())
            return;

        let headers = new Headers();
        headers.append("Content-Type", "application/x-www-form-urlencoded");

        let params = new URLSearchParams();
        params.append("client_id", "foodnetClient");
        params.append("grant_type", "password");
        params.append("scope", "api");
        params.append("username", this.state.username);
        params.append("password", this.state.password);

        let request: RequestInit;
        request = {
            method: "POST",
            headers: headers,
            body: params
        };
        let result = await fetch(cfg.AuthEndpoint + "/connect/token", request);

        if (result.ok) {

            let tokenResult = await result.json();
            httpClient.accessToken = tokenResult.access_token;
            this.props.history.push(routes.FridgePage);
        }
    }
}
