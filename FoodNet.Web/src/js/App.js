"use strict";
var __extends = (this && this.__extends) || (function () {
    var extendStatics = Object.setPrototypeOf ||
        ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
        function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
Object.defineProperty(exports, "__esModule", { value: true });
var React = require("react");
var react_router_dom_1 = require("react-router-dom");
var routes = require("./Routes");
var Videos_1 = require("./Components/Videos");
var AddVideo_1 = require("./Components/AddVideo");
var App = (function (_super) {
    __extends(App, _super);
    function App(props) {
        return _super.call(this, props) || this;
    }
    App.prototype.render = function () {
        return (React.createElement("div", null, React.createElement(react_router_dom_1.Switch, null,
            React.createElement(react_router_dom_1.Route, { exact: true, path: routes.Videos, component: Videos_1.Videos }),
            React.createElement(react_router_dom_1.Route, { exact: true, path: routes.AddVideo, component: AddVideo_1.AddVideo }))));
    };
    return App;
}(React.Component));
exports.App = App;
//# sourceMappingURL=App.js.map