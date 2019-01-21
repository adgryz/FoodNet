import * as React from "react";
import * as ReactDOM from "react-dom";

import { App } from "./App";
import { BrowserRouter } from "react-router-dom";
import { MuiThemeProvider, MuiTheme, getMuiTheme } from "material-ui/styles";
import { purple800, limeA400, blueA700, grey900, white, grey50, green600, purple600, blue500 } from "material-ui/styles/colors";

var injectTapEventPlugin = require("react-tap-event-plugin");
injectTapEventPlugin();

// We tested different color plaettes, but default was ok

const theme: MuiTheme = {
    palette: {
        primary1Color: blue500,
        primary2Color: blue500,
        primary3Color: blue500,
    }
};

const muiTheme = getMuiTheme(theme);

const app = <MuiThemeProvider muiTheme={muiTheme}>
    <BrowserRouter>
        <App />
    </BrowserRouter>
</MuiThemeProvider>;

let render = () => ReactDOM.render(app, document.getElementById("root"));

render();

declare var module: { hot: any };
if (module.hot) {
    module.hot.accept();
}

