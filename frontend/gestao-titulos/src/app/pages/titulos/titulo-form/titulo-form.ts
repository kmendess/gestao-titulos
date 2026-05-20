import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { NgxMaskDirective } from 'ngx-mask';
import { TituloService } from '../../../services/titulo-service';
import { TituloRequest } from '../../../models/titulo-request';
import { Titulo } from '../../../models/titulo';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';

@Component({
  selector: 'app-titulo-form',
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatDialogModule,
    NgxMaskDirective,
    MatIconModule,
    MatCardModule,
    MatSnackBarModule
  ],
  templateUrl: './titulo-form.html',
  styleUrl: './titulo-form.css',
})
export class TituloForm implements OnInit {
  private fb = inject(FormBuilder);
  private readonly dialogRef = inject(MatDialogRef<TituloForm>);
  readonly data = inject(MAT_DIALOG_DATA, { optional: true });
  readonly isDetail = !!this.data;
  private readonly tituloService = inject(TituloService);
  private readonly snackBar = inject(MatSnackBar);

  form = this.fb.group({
    numeroTitulo: [
      { value: '', disabled: this.isDetail },
      [
        Validators.required,
        Validators.maxLength(50),
        Validators.pattern(/^[a-zA-Z0-9]+$/)
      ]
    ],
    nomeDevedor: [
      { value: '', disabled: this.isDetail },
      [
        Validators.required,
        Validators.maxLength(150)
      ]
    ],
    cpfDevedor: [
      { value: '', disabled: this.isDetail },
      [
        Validators.required,
        Validators.pattern(/^\d{11}$/)
      ]
    ],
    percentualJuros: [
      { value: 0, disabled: this.isDetail },
      [
        Validators.required,
        Validators.min(0),
        Validators.max(100)
      ]
    ],
    percentualMulta: [
      { value: 0, disabled: this.isDetail },
      [
        Validators.required,
        Validators.min(0),
        Validators.max(100)
      ]
    ],
    parcelas: this.fb.array([])
  });

  get parcelas(): FormArray {
    return this.form.get('parcelas') as FormArray;
  }

  constructor() {
    if (!this.isDetail) {
      this.adicionarParcela();
    }
  }

  ngOnInit() {
    if (this.isDetail) {
      this.carregarTitulo(this.data);
    }
  }

  private carregarTitulo(titulo: Titulo): void {
    if (titulo) {
      this.form.patchValue({
        numeroTitulo: titulo.numeroTitulo,
        nomeDevedor: titulo.nomeDevedor,
        cpfDevedor: titulo.cpfDevedor,
        percentualJuros: titulo.percentualJuros,
        percentualMulta: titulo.percentualMulta
      });

      this.parcelas.clear();

      titulo.parcelas.forEach(parcela => {
        this.parcelas.push(
          this.fb.group({
            numeroParcela: [
              { value: parcela.numeroParcela, disabled: true }
            ],
            dataVencimento: [
              { value: parcela.dataVencimento, disabled: true }
            ],
            valor: [
              { value: parcela.valorParcela, disabled: true }
            ]
          })
        );
      });
    }
  }

  fechar(): void {
    this.dialogRef.close();
  }

  salvar(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    const titulo = this.form.getRawValue() as TituloRequest;

    this.tituloService
      .criar(titulo)
      .subscribe({
        next: () => {
          this.snackBar.open(
            'Título cadastrado com sucesso.',
            'Fechar',
            { duration: 4000, verticalPosition: 'top' });

          this.dialogRef.close(true);
        },
        error: error => {
          const messages = error?.error?.messages;
          this.snackBar.open(
            messages?.join('\n') ?? 'Erro ao cadastrar título.',
            'Fechar',
            { duration: 4000, verticalPosition: 'top' });
        }
      });
  }

  private criarParcela(): FormGroup {
    return this.fb.group({
      numeroParcela: [
        { value: null, disabled: this.isDetail },
        [
          Validators.required,
          Validators.min(1)
        ]
      ],
      dataVencimento: [
        { value: null, disabled: this.isDetail },
        [Validators.required]
      ],
      valor: [
        { value: null, disabled: this.isDetail },
        [
          Validators.required,
          Validators.min(0.01)
        ]
      ]
    });
  }

  adicionarParcela(): void {
    this.parcelas.push(
      this.criarParcela());
  }

  removerParcela(index: number): void {
    this.parcelas.removeAt(index);
  }
}
