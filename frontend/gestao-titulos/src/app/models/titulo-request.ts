import { ParcelaRequest } from "./parcela-request";

export interface TituloRequest {
  numeroTitulo: string;
  nomeDevedor: string;
  cpfDevedor: string;
  percentualJuros: number;
  percentualMulta: number;
  parcelas: ParcelaRequest[];
}
