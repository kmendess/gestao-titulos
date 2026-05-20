import { Routes } from '@angular/router';

export const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: 'home', loadComponent: () => import('./pages/home/home').then(m => m.Home) },
  { path: 'titulos', loadComponent: () => import('./pages/titulos/titulo-list/titulo-list').then(m => m.TituloList) }
];
