const webpack = require("webpack");
const path = require("path");

const Merge = require('webpack-merge');
const CommonConfig = require('./webpack.common.js');

module.exports = Merge(CommonConfig, {
    devtool: "source-map",
    resolve: {
        alias: {
            config: path.join(__dirname, "./src/js/Configuration/Config.dev.ts"),
        }
    },
});

