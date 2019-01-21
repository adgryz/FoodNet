const webpack = require("webpack");

module.exports = function (environment) {
    const env = "development";

    return (env === "development" && require('./webpack.dev.js'))

}
