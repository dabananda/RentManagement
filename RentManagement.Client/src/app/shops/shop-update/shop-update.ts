import { Component, inject, OnInit, signal } from '@angular/core';
import { ShopService } from '../../_services/shop.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ShopUpdate } from '../../_models/shop';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-shop-update',
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './shop-update.html',
  styleUrl: './shop-update.css',
})
export class ShopUpdateComponent implements OnInit {
  private shopService = inject(ShopService);
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private fb = inject(FormBuilder);

  shop = signal<ShopUpdate | null>(null);
  shopForm!: FormGroup;
  loading = signal<boolean>(false);
  error = signal<string | null>(null);

  ngOnInit(): void {
    this.initializeForm();
    this.loadShop();
  }

  initializeForm(): void {
    this.shopForm = this.fb.group({
      shopNumber: ['', [Validators.required]],
      areaSqFt: [0, [Validators.required, Validators.min(1)]],
      floor: ['', [Validators.required]],
    });
  }

  loadShop(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    if (!id) {
      this.error.set('Invalid shop ID');
      return;
    }

    this.loading.set(true);
    this.shopService.getShopForUpdate(id).subscribe({
      next: (shop) => {
        this.shop.set(shop);
        this.shopForm.patchValue({
          shopNumber: shop.shopNumber,
          areaSqFt: shop.areaSqFt,
          floor: shop.floor,
        });
        this.loading.set(false);
      },
      error: (error) => {
        console.error('Error loading shop:', error);
        this.error.set('Failed to load shop details');
        this.loading.set(false);
      },
    });
  }

  onSubmit(): void {
    if (this.shopForm.invalid) {
      this.shopForm.markAllAsTouched();
      return;
    }

    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.loading.set(true);
    this.error.set(null);

    this.shopService.updateShop(id, this.shopForm.value).subscribe({
      next: () => {
        this.loading.set(false);
        this.router.navigate(['/shops']);
      },
      error: (error) => {
        console.error('Error updating shop:', error);
        this.error.set('Failed to update shop');
        this.loading.set(false);
      },
    });
  }

  cancel(): void {
    this.router.navigate(['/shops']);
  }
}
