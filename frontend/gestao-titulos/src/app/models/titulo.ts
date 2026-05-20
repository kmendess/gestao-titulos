import { Parcela } from "./parcela";

export interface Titulo {
  numeroTitulo: string;
  nomeDevedor: string;
  cpfDevedor: string;
  percentualJuros: number;
  percentualMulta: number;
  quantidadeParcelas: number;
  valorOriginal: number;
  valorMulta: number;
  valorJuros: number;
  valorAtualizado: number;
  diasEmAtraso: number;
  parcelas: Parcela[];
}
