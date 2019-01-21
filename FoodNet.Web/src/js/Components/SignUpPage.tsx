import * as React from "react";
import { Card, CardHeader, CardActions, CardText, TextField, RadioButton, Checkbox, RaisedButton, FloatingActionButton } from "material-ui";
import { ContentAddCircle, ContentAdd } from "material-ui/svg-icons";
import * as Router from "react-router-dom";
import * as routes from "../Routes";
import axios from "axios";
import { Link } from "react-router-dom";
import { httpClient } from "../Infrastructure/httpsClient";
import { SingUpDTO } from "../Conracts";
import cfg from "config";

// SIGNUP
// SIGNIN

export interface SignUpPageProps {
}

interface SignUpPageState {
    readonly username: string;
    readonly password: string;
    readonly passwordRep: string;
    readonly email: string;
    readonly emailRep: string;
    readonly nameError: string;
    readonly passwordError: string;
    readonly passwordRepError: string;
    readonly emailError: string;
    readonly emailRepError: string;
}

type SignUpPageWithRouterProps = Router.RouteComponentProps<{}> & SignUpPageProps;


export class SignUpPage extends React.Component<SignUpPageWithRouterProps, SignUpPageState> {
    public constructor(props: SignUpPageWithRouterProps) {
        super(props);
        this.state = {
            username: "",
            nameError: "",
            password: "",
            passwordRep: "",
            passwordRepError: "",
            email: "",
            emailRep: "",
            emailRepError: "",
            passwordError: "",
            emailError: "",
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
                            errorText={this.state.nameError}
                            value={this.state.username}
                            floatingLabelText="Username"
                            onChange={(_, username) => this.setState({ username })}
                        /><br />
                        <TextField
                            value={this.state.password}
                            floatingLabelText="Password"
                            type="password"
                            errorText={this.state.passwordError}
                            onChange={(_, password) => this.setState({ password })}
                        /><br />
                        <TextField
                            errorText={this.state.passwordRepError}
                            value={this.state.passwordRep}
                            floatingLabelText="Repeat Password"
                            type="password"
                            onChange={(_, passwordRep) => this.setState({ passwordRep })}
                        /><br />
                        <TextField
                            errorText={this.state.emailError}
                            value={this.state.email}
                            floatingLabelText="E-mail"
                            onChange={(_, email) => this.setState({ email })}
                        /><br />
                        <TextField
                            errorText={this.state.emailRepError}
                            value={this.state.emailRep}
                            floatingLabelText="Repeat E-mail"
                            onChange={(_, emailRep) => this.setState({ emailRep })}
                        /><br />
                        <RaisedButton
                            primary={true}
                            label="SIGN UP"
                            onClick={this.signUp} />
                    </div>
                </Card>
            </div>
        );
    }

    private validate = () => {
        if (this.state.username === "") {
            this.setState({ nameError: "Empty username" });
            return false;
        }

        if (this.state.password.length < 6) {
            this.setState({ passwordError: "Too short" });
            return false;
        }

        if (this.state.password.search(/\d/) == -1) {
            this.setState({ passwordError: "No number" });
            return false;
        }

        if (this.state.password.search(/[a-zA-Z]/) == -1) {
            this.setState({ passwordError: "No letter" });
            return false;
        }

        if (this.state.password.search(/[A-Z]/) == -1) {
            this.setState({ passwordError: "No capital letter" });
            return false;
        }

        if (this.state.password.search(/[^A-Za-z0-9 ]/) == -1) {
            this.setState({ passwordError: "No non aplhanumeric characters" });
            return false;
        }

        if (this.state.password !== this.state.passwordRep) {
            this.setState({ passwordRepError: "Passwords must match" });
            return false;
        }

        var emailRegex = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        if (!emailRegex.test(this.state.email.toLowerCase())) {
            this.setState({ emailError: "Wrong email" });
            return false;
        }

        if (this.state.email !== this.state.emailRep) {
            this.setState({ emailRepError: "Emails must match" });
            return false;
        }

        return true;
    }

    private clearErrors = () => this.setState({
        passwordError: "",
        passwordRepError: "",
        nameError: "",
        emailError: "",
        emailRepError: ""
    });

    private signUp = async () => {
        let result = true;

        this.clearErrors();

        if (this.validate()) {
            let data: SingUpDTO;
            data = {
                Email: this.state.email,
                Password: this.state.password,
                Name: this.state.username
            };
            await httpClient.executeRequest("POST", "/account/signup", data);
            await this.tryLogin()
        }

    }
    private tryLogin = async () => {

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
