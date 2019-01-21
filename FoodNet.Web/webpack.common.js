const webpack = require("webpack");
const path = require("path");

const ExtractTextPlugin = require("extract-text-webpack-plugin");
const HtmlWebpackPlugin = require("html-webpack-plugin");
const CopyWebpackPlugin = require("copy-webpack-plugin");

const deployDir = process.env.FRONT_DEPLOY_DIR ? process.env.FRONT_DEPLOY_DIR : __dirname;

module.exports = {
    entry: [
        "./src/css/app.sass",
        "./src/js/index.tsx"
    ],

    output: {
        path: path.join(deployDir, "dist"),
        filename: "[name].js",
        publicPath: "/"
    },

    resolve: {
        extensions: [".ts", ".tsx", ".js"]
    },


    plugins: [
        new ExtractTextPlugin({ filename: "styles.css", allChunks: true }),

        new webpack.optimize.CommonsChunkPlugin({
            name: 'vendor',
            minChunks: function (module) {
                return module.context && module.context.indexOf('node_modules') !== -1;
            }
        }),
        new HtmlWebpackPlugin({
            template: path.join(__dirname, "index.html"),
        }),
    ],

    module: {
        rules: [
            {
                test: /\.(ts|tsx)$/,
                loader: "awesome-typescript-loader",
                exclude: path.resolve(__dirname, "node_modules"),
                include: path.resolve(__dirname, "src/js"),
            },
            {
                test: /\.sass$/,
                use: ExtractTextPlugin.extract({
                    fallback: "style-loader", use: "css-loader!sass-loader",
                })
            },
            {
                test: /\.(woff2?)$/,
                loader: "url-loader?limit=10000"
            },
            {
                test: /\.(ttf|eot|svg|gif)(\?[\s\S]+)?$/,
                loader: "file-loader?name=fonts/[name].[ext]",
            },
            {
                test: /\.(jpg|png)$/,
                loader: "url-loader?limit=1024&name=images/[name].[ext]",
                exclude: path.resolve(__dirname, "node_modules"),
                include: path.resolve(__dirname, "src/images")
            }
        ]
    }
}

