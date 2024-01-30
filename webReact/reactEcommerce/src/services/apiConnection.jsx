import { api, requestConfig } from "../utils/config";

export const viaCep = axios.create({
    baseURL: 'https://viacep.com.br/ws/'
})