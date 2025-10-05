import { Component, inject, OnInit, signal } from '@angular/core';
import { ShopService } from '../../_services/shop.service';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-shop-list',
  imports: [RouterLink],
  templateUrl: './shop-list.html',
  styleUrl: './shop-list.css',
})
export class ShopList implements OnInit {
  shopService = inject(ShopService);

  showDeleteModal = signal<boolean>(false);
  shopToDelete = signal<{ id: number; shopNumber: string } | null>(null);
  deleting = signal<number | null>(null);
  error = signal<string | null>(null);
  successMessage = signal<string | null>(null);

  ngOnInit(): void {
    if (this.shopService.shops().length === 0) {
      this.shopService.getShops();
    }
  }

  confirmDelete(id: number, shopNumber: string): void {
    this.shopToDelete.set({ id, shopNumber });
    this.showDeleteModal.set(true);
  }

  cancelDelete(): void {
    this.showDeleteModal.set(false);
    this.shopToDelete.set(null);
  }

  deleteShop(): void {
    const shop = this.shopToDelete();
    if (!shop) return;

    this.deleting.set(shop.id);
    this.showDeleteModal.set(false);
    this.error.set(null);
    this.successMessage.set(null);

    this.shopService.deleteShop(shop.id).subscribe({
      next: () => {
        this.deleting.set(null);
        this.shopToDelete.set(null);
        this.successMessage.set(`Shop ${shop.shopNumber} deleted successfully`);

        setTimeout(() => {
          this.successMessage.set(null);
        }, 3000);
      },
      error: (error) => {
        console.error('Error deleting shop:', error);
        this.deleting.set(null);
        this.shopToDelete.set(null);
        this.error.set(`Failed to delete shop ${shop.shopNumber}`);
      },
    });
  }
}
