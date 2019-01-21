import * as React from "react";
import { Card, CardHeader, CardActions, CardText, TextField, RadioButton, Checkbox, RaisedButton, FloatingActionButton } from "material-ui";
import { ContentAddCircle, ContentAdd } from "material-ui/svg-icons";
import * as Router from "react-router-dom";
import * as routes from "../Routes";
import axios from "axios";
import { Link } from "react-router-dom";
import cfg from "config";
import { httpClient } from "../Infrastructure/httpsClient";


export function NotFoundPage() {
    return (
        <div style={{ width: "1000px", height: "750px", margin: "auto", marginTop: "100px", position: "relative" }}>
            <img style={{ width: "1000px", height: "750px", margin: "auto", position: "relative" }} src="https://memegenerator.net/img/instances/500x/48030660/wow-such-error-so-not-found-much-404.jpg" />
        </div>
    );
}

