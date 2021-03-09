const path = require("path");

module.exports = {
  module: {
    rules: [
      {
        test: /\.(js|jsx)$/,
        exclude: /node_modules/,
        use: {
          loader: "babel-loader"
        }
      }
    ]
  },
  entry: path.resolve(__dirname, 'wwwroot') + '/js/src/index.js',
  output: {
    path: path.resolve(__dirname, 'wwwroot/js/dist'),
    filename: "app.js"
  }
};
