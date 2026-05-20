import { Component, inject } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-titulo-delete-dialog',
  imports: [
    MatDialogModule,
    MatButtonModule
  ],
  templateUrl: './titulo-delete-dialog.html',
  styleUrl: './titulo-delete-dialog.css',
})
export class TituloDeleteDialog {
  readonly dialogRef = inject(MatDialogRef<TituloDeleteDialog>);

  readonly data = inject(MAT_DIALOG_DATA);

  confirmar(): void {
    this.dialogRef.close(true);
  }
}
