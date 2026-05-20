import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TituloDeleteDialog } from './titulo-delete-dialog';

describe('TituloDeleteDialog', () => {
  let component: TituloDeleteDialog;
  let fixture: ComponentFixture<TituloDeleteDialog>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TituloDeleteDialog],
    }).compileComponents();

    fixture = TestBed.createComponent(TituloDeleteDialog);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
