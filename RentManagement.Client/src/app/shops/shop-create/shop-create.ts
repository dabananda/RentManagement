import { Component, inject, OnInit, signal } from '@angular/core';
import { ShopService } from '../../_services/shop.service';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-shop-create',
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './shop-create.html',
  styleUrl: './shop-create.css',
})
export class ShopCreate implements OnInit {
  private shopService = inject(ShopService);
  private router = inject(Router);
  private fb = inject(FormBuilder);

  shopForm!: FormGroup;
  loading = signal<boolean>(false);
  error = signal<string | null>(null);

  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm(): void {
    this.shopForm = this.fb.group({
      shopNumber: ['', [Validators.required]],
      areaSqFt: [0, [Validators.required, Validators.min(1)]],
      floor: ['', [Validators.required]],
    });
  }

  onSubmit(): void {
    if (this.shopForm.invalid) {
      this.shopForm.markAllAsTouched();
      return;
    }

    this.loading.set(true);
    this.error.set(null);

    this.shopService.createShop(this.shopForm.value).subscribe({
      next: () => {
        this.loading.set(false);
        this.router.navigate(['/shops']);
      },
      error: (error) => {
        console.error('Error creating shop:', error);
        this.error.set('Failed to create shop');
        this.loading.set(false);
      },
    });
  }

  cancel(): void {
    this.router.navigate(['/shops']);
  }
}
