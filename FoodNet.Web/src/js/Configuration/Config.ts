interface Configuration {
    AuthEndpoint: string;
    AuthSecret: string;
    HttpsEndpoint: string;
}

declare module "config" {
    const cfg: Configuration;

    export default cfg;
}
