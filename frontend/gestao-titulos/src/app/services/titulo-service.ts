import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import { Titulo } from '../models/titulo';
import { TituloRequest } from '../models/titulo-request';
import { Result } from '../models/result';

@Injectable({
  providedIn: 'root',
})
export class TituloService {
  constructor(private http: HttpClient) { }

  private readonly apiUrl = `${environment.apiURL}/titulos`;

  obterTodos(): Observable<Titulo[]> {
    return this.http.get<Titulo[]>(this.apiUrl);
  }

  criar(titulo: TituloRequest): Observable<Result<void>> {
    return this.http.post<Result<void>>(this.apiUrl, titulo);
  }

  excluir(numeroTitulo: string): Observable<Result<void>> {
    return this.http.delete<Result<void>>(`${this.apiUrl}/${numeroTitulo}`);
  }
}
