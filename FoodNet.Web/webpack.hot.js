const webpack = require("webpack");
const path = require("path");

const Merge = require('webpack-merge');
const CommonConfig = require('./webpack.common.js');

const ExtractTextPlugin = require("extract-text-webpack-plugin");
const HtmlWebpackPlugin = require("html-webpack-plugin");
const CopyWebpackPlugin = require("copy-webpack-plugin");

const express = require('express');
const app = express();

app.get('/', function (req, res) {
    res.send('Hello World!')
});

module.exports = Merge(CommonConfig, {
    entry: [
        "webpack-dev-server/client?http://localhost:5001",
        "webpack/hot/only-dev-server"
    ],

    devtool: "source-map",

    resolve: {
        alias: {
            config: path.join(__dirname, "./src/js/Configuration/Config.dev.ts"),
        }
    },

    plugins: [
        new webpack.HotModuleReplacementPlugin(),
        new webpack.NamedModulesPlugin(),
        new webpack.NoEmitOnErrorsPlugin(),

        new HtmlWebpackPlugin({
            template: path.join(__dirname, "index.html")
        })
    ],

    devServer: {
        hot: true,
        publicPath: "/",

        host: "localhost",
        port: 5001,
        historyApiFallback: true,
    }
})
