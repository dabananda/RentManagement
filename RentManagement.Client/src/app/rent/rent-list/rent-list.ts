import { Component, inject, OnInit, signal } from '@angular/core';
import { RentService } from '../../_services/rent.service';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-rent-list',
  imports: [ReactiveFormsModule, DatePipe],
  templateUrl: './rent-list.html',
  styleUrl: './rent-list.css',
})
export class RentList implements OnInit {
  rentService = inject(RentService);
  private router = inject(Router);
  private fb = inject(FormBuilder);

  rentForm!: FormGroup;

  showRecordModal = signal<boolean>(false);
  loading = signal<boolean>(false);
  error = signal<string | null>(null);
  successMessage = signal<string | null>(null);

  ngOnInit(): void {
    this.initForm();
    this.loadRents();
  }

  loadRents() {
    this.rentService.getRents();
  }

  initForm(): void {
    this.rentForm = this.fb.group({
      shopId: ['', [Validators.required, Validators.min(1)]],
      month: ['', [Validators.min(1), Validators.max(12)]],
      year: ['', [Validators.min(2000)]],
      amount: ['', [Validators.min(0)]],
      notes: [''],
    });
  }

  onSubmit(): void {
    if (this.rentForm.invalid) {
      this.rentForm.markAllAsTouched();
      return;
    }

    this.loading.set(true);
    this.error.set(null);

    const rentData = {
      shopId: Number(this.rentForm.value.shopId),
      month: this.rentForm.value.month ? Number(this.rentForm.value.month) : null,
      year: this.rentForm.value.year ? Number(this.rentForm.value.year) : null,
      amount: this.rentForm.value.amount ? Number(this.rentForm.value.amount) : null,
      notes: this.rentForm.value.notes || null,
    };

    this.rentService.addRent(rentData).subscribe({
      next: () => {
        this.loading.set(false);
        this.successMessage.set('Rent recorded successfully!');
        this.rentForm.reset();
        this.loadRents();
        const modalElement = document.getElementById('add-rent');
        const modal = (window as any).bootstrap?.Modal?.getInstance(modalElement);
        modal?.hide();
      },
      error: (error) => {
        const errorMessage =
          error.error?.message || error.error?.Message || error.message || 'Failed to record rent';
        this.error.set(errorMessage);
        this.loading.set(false);
      },
    });
  }
}
