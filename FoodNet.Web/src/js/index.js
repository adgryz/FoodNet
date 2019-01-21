"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var React = require("react");
var ReactDOM = require("react-dom");
var App_1 = require("./App");
var react_router_dom_1 = require("react-router-dom");
var styles_1 = require("material-ui/styles");
var app = React.createElement(styles_1.MuiThemeProvider, null,
    React.createElement(react_router_dom_1.BrowserRouter, null,
        React.createElement(App_1.App, null)));
var render = function () { return ReactDOM.render(app, document.getElementById("root")); };
render();
if (module.hot) {
    module.hot.accept();
}
//# sourceMappingURL=index.js.map