import { CommonModule } from '@angular/common';
import { ChangeDetectorRef, Component, inject } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { Titulo } from '../../../models/titulo';
import { TituloService } from '../../../services/titulo-service';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { finalize } from 'rxjs';
import { TituloForm } from '../titulo-form/titulo-form';
import { MatTooltipModule } from '@angular/material/tooltip';
import { TituloDeleteDialog } from '../titulo-delete-dialog/titulo-delete-dialog';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';

@Component({
  selector: 'app-titulo-list',
  imports: [
    CommonModule,
    MatButtonModule,
    MatTableModule,
    MatIconModule,
    MatProgressSpinnerModule,
    MatTooltipModule,
    MatDialogModule,
    MatSnackBarModule
  ],
  templateUrl: './titulo-list.html',
  styleUrl: './titulo-list.css',
})
export class TituloList {
  colunas: string[] = [
    'numeroTitulo',
    'nomeDevedor',
    'quantidadeParcelas',
    'valorOriginal',
    'diasEmAtraso',
    'valorAtualizado',
    'acoes'
  ];
  titulos: Titulo[] = [];
  loading = false;
  private readonly snackBar = inject(MatSnackBar);

  constructor(
    private tituloService: TituloService,
    private dialog: MatDialog,
    private cdr: ChangeDetectorRef
  ) { }

  ngOnInit() {
    this.obterTitulos();
  }

  obterTitulos(): void {
    this.loading = true;

    this.tituloService.obterTodos()
      .pipe(
        finalize(() => {
          this.loading = false;
          this.cdr.detectChanges();
        })
      )
      .subscribe({
        next: (result: Titulo[]) => {
          this.titulos = [...result];
        },
        error: () => {
          this.titulos = [];
        }
      });
  }

  abrirCadastro(): void {
    const dialogRef = this.dialog.open(TituloForm, {
      maxWidth: '90vh'
    });

    dialogRef.afterClosed()
      .subscribe(result => {
        if (result) {
          this.obterTitulos();
        }
      });
  }

  abrirDetalhes(titulo: Titulo): void {
    this.dialog.open(TituloForm, {
      maxWidth: '90vh',
      data: titulo
    });
  }

  confirmarExclusao(numeroTitulo: string): void {
    const dialogRef = this.dialog.open(
      TituloDeleteDialog,
      { width: '400px' }
    );

    dialogRef.afterClosed()
      .subscribe(result => {
        if (!result) {
          return;
        }

        this.excluir(numeroTitulo);
      });
  }

  private excluir(numeroTitulo: string): void {
    this.tituloService
      .excluir(numeroTitulo)
      .subscribe({
        next: () => {
          this.snackBar.open(
            'Título excluído com sucesso.',
            'Fechar',
            { duration: 4000, verticalPosition: 'top' });

          this.obterTitulos();
        },
        error: error => {
          const messages = error?.error?.messages;
          this.snackBar.open(
            messages?.join('\n') ?? 'Erro ao excluir título.',
            'Fechar',
            { duration: 4000, verticalPosition: 'top' });
        }
      });
  }
}
