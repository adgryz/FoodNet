const webpack = require("webpack");
const path = require("path");

const Merge = require('webpack-merge');
const CommonConfig = require('./webpack.common.js');
const CleanWebpackPlugin = require('clean-webpack-plugin');
const CopyWebpackPlugin = require("copy-webpack-plugin");


const deployDir = process.env.WEB_DEPLOY_DIR ? process.env.WEB_DEPLOY_DIR : __dirname;

module.exports = Merge(CommonConfig, {
    plugins: [
        new webpack.DefinePlugin({
            'process.env': {
                NODE_ENV: JSON.stringify('production')
            }
        }),

        new CleanWebpackPlugin([deployDir]),
        new webpack.LoaderOptionsPlugin({
            minimize: true,
            debug: false
        }),
        new webpack.optimize.UglifyJsPlugin({
            beautify: false,
            mangle: {
                screw_ie8: true,
                keep_fnames: true
            },
            compress: {
                screw_ie8: true
            },
            comments: false
        }),
        new CopyWebpackPlugin([
            { from: 'web.config' }
        ])
    ],

    resolve: {
        alias: {
            config: path.join(__dirname, "./src/js/Configuration/Config.prod.ts"),
        }
    },
});
