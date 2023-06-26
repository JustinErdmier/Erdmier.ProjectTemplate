module.exports = ({env}) => ({
    plugins: {
        tailwindcss:  {},
        autoprefixer: {},
        cssnano:      env === "release" ? {preset: "default"} : false
    }
});
