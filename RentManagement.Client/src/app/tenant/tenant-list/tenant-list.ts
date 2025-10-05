import { Component, inject, OnInit, signal } from '@angular/core';
import { TenantService } from '../../_services/tenant.service';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-tenant-list',
  imports: [RouterLink],
  templateUrl: './tenant-list.html',
  styleUrl: './tenant-list.css',
})
export class TenantList implements OnInit {
  tenantService = inject(TenantService);

  showDeleteModal = signal<boolean>(false);
  tenantToDelete = signal<{ id: number; name: string } | null>(null);
  deleting = signal<number | null>(null);
  error = signal<string | null>(null);
  successMessage = signal<string | null>(null);

  ngOnInit(): void {
    this.loadTenants();
  }

  loadTenants() {
    this.tenantService.getTenants();
  }

  confirmDelete(id: number, name: string): void {
    this.tenantToDelete.set({ id, name });
    this.showDeleteModal.set(true);
  }

  cancelDelete(): void {
    this.showDeleteModal.set(false);
    this.tenantToDelete.set(null);
  }

  deleteTenant(): void {
    const shop = this.tenantToDelete();
    if (!shop) return;

    this.deleting.set(shop.id);
    this.showDeleteModal.set(false);
    this.error.set(null);
    this.successMessage.set(null);

    this.tenantService.deleteTenant(shop.id).subscribe({
      next: () => {
        this.deleting.set(null);
        this.tenantToDelete.set(null);
        this.successMessage.set(`Shop ${shop.name} deleted successfully`);

        setTimeout(() => {
          this.successMessage.set(null);
        }, 3000);
      },
      error: (error) => {
        console.error('Error deleting shop:', error);
        this.deleting.set(null);
        this.tenantToDelete.set(null);
        this.error.set(`Failed to delete shop ${shop.name}`);
      },
    });
  }
}
