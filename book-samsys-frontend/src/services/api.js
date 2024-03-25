import axios from "axios";

export const variables={
    API_URL: "https://localhost:7063/api/"
}

const api = axios.create({
    baseURL: "https://localhost:7063/api/",
});
export default api;