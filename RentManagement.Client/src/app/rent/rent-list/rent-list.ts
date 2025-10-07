import { Component, inject, OnInit, signal } from '@angular/core';
import { RentService } from '../../_services/rent.service';
import { ShopService } from '../../_services/shop.service';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CurrencyPipe, DatePipe } from '@angular/common';

@Component({
  selector: 'app-rent-list',
  imports: [ReactiveFormsModule, DatePipe, CurrencyPipe],
  templateUrl: './rent-list.html',
  styleUrl: './rent-list.css',
})
export class RentList implements OnInit {
  rentService = inject(RentService);
  shopService = inject(ShopService);
  private fb = inject(FormBuilder);

  rentForm!: FormGroup;

  showRecordModal = signal<boolean>(false);
  loading = signal<boolean>(false);
  error = signal<string | null>(null);
  successMessage = signal<string | null>(null);
  deleteLoading = signal<boolean>(false);
  rentToDelete = signal<number | null>(null);
  selectedRent = signal<number | null>(null);
  rentDetails = signal<any>(null);

  ngOnInit(): void {
    this.initForm();
    this.loadRents();
    this.loadShops();
  }

  loadRents() {
    this.rentService.getRents();
  }

  loadShops() {
    this.shopService.getShops();
  }

  initForm(): void {
    this.rentForm = this.fb.group({
      shopId: ['', [Validators.required]],
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
    this.successMessage.set(null);

    const rentData = {
      shopId: Number(this.rentForm.value.shopId),
      month: this.rentForm.value.month || null,
      year: this.rentForm.value.year || null,
      amount: this.rentForm.value.amount ? Number(this.rentForm.value.amount) : null,
      notes: this.rentForm.value.notes || null,
    };

    this.rentService.addRent(rentData).subscribe({
      next: () => {
        this.loading.set(false);
        this.successMessage.set('Rent recorded successfully!');
        this.rentForm.reset();
        this.loadRents();

        // Auto-dismiss success message after 3 seconds
        setTimeout(() => {
          this.successMessage.set(null);
        }, 3000);

        // Close modal after successful submission
        const modalElement = document.getElementById('add-rent');
        const modal = (window as any).bootstrap.Modal.getInstance(modalElement);
        if (modal) {
          modal.hide();
        }
      },
      error: (error) => {
        const errorMessage =
          error.error?.message || error.error?.Message || error.message || 'Failed to record rent';
        this.error.set(errorMessage);
        this.loading.set(false);
      },
    });
  }

  openDeleteModal(rentId: number): void {
    this.rentToDelete.set(rentId);
    this.error.set(null);
  }

  openDetailsModal(rentId: number): void {
    this.selectedRent.set(rentId);
    this.rentService.getRent(rentId).subscribe({
      next: (rent) => {
        this.rentDetails.set(rent);
      },
      error: (error) => {
        const errorMessage =
          error.error?.message ||
          error.error?.Message ||
          error.message ||
          'Failed to load rent details';
        this.error.set(errorMessage);
      },
    });
  }

  closeDetailsModal(): void {
    this.selectedRent.set(null);
    this.rentDetails.set(null);
  }

  confirmDelete(): void {
    const rentId = this.rentToDelete();
    if (!rentId) return;

    this.deleteLoading.set(true);
    this.error.set(null);
    this.successMessage.set(null);

    this.rentService.deleteRent(rentId).subscribe({
      next: () => {
        this.deleteLoading.set(false);
        this.successMessage.set('Rent record deleted successfully!');
        this.rentToDelete.set(null);
        this.loadRents();

        // Auto-dismiss success message after 3 seconds
        setTimeout(() => {
          this.successMessage.set(null);
        }, 3000);

        // Close modal after successful deletion
        const modalElement = document.getElementById('delete-rent-modal');
        const modal = (window as any).bootstrap.Modal.getInstance(modalElement);
        if (modal) {
          modal.hide();
        }
      },
      error: (error) => {
        const errorMessage =
          error.error?.message ||
          error.error?.Message ||
          error.message ||
          'Failed to delete rent record';
        this.error.set(errorMessage);
        this.deleteLoading.set(false);
      },
    });
  }

  cancelDelete(): void {
    this.rentToDelete.set(null);
    this.error.set(null);
  }
}
