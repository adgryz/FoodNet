import cfg from "config";
import * as moment from "moment";

export class HttpClient {
    private apiEndpoint: string;
    public accessToken: string;

    public constructor(apiEndpoint: string) {
        this.apiEndpoint = apiEndpoint;
    }

    public executeRequest(type: string, requestName: string, dto: any): Promise<any> {
        const path = this.apiEndpoint + requestName;
        return this.makeRequest(path, dto, type);
    }

    private async makeRequest(url: string, dto: any, type: string): Promise<any> {
        let init = await this.prepareRequest(dto, type);
        let result = await fetch(url, init);
        return await result.json();
    }


    private async prepareRequest(dto: any, type: string): Promise<RequestInit> {
        let headers = new Headers();
        headers.append("Content-Type", "application/json");
        headers.append("Authorization", "Bearer " + this.accessToken);
        return type === "GET"
            ? {
                method: type,
                headers: headers
            }
            :
            {
                method: type,
                body: JSON.stringify(dto),
                headers: headers
            };

    }
}

export const httpClient = new HttpClient(cfg.HttpsEndpoint);
