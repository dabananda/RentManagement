import { Component, OnInit, inject, signal, computed } from '@angular/core';
import { RouterLink } from '@angular/router';
import { AgreementService } from '../../_services/agreement.service';

@Component({
  selector: 'app-agreement-list',
  imports: [RouterLink],
  templateUrl: './agreement-list.html',
  styleUrl: './agreement-list.css',
})
export class AgreementList implements OnInit {
  agreementService = inject(AgreementService);

  showDeleteModal = signal<boolean>(false);
  agreementToDelete = signal<{ id: number } | null>(null);
  deleting = signal<number | null>(null);
  error = signal<string | null>(null);
  successMessage = signal<string | null>(null);

  ngOnInit(): void {
    this.loadAgreements();
  }

  loadAgreements(): void {
    this.agreementService.getAgreements();
  }

  confirmDelete(id: number): void {
    this.agreementToDelete.set({ id });
    this.showDeleteModal.set(true);
  }

  cancelDelete(): void {
    this.showDeleteModal.set(false);
    this.agreementToDelete.set(null);
  }

  deleteAgreement() {
    const target = this.agreementToDelete();
    if (!target) return;

    const id = target.id;
    this.deleting.set(id);

    this.agreementService.deleteAgreement(id).subscribe({
      next: () => {
        if (this.agreementService?.agreements?.update) {
          this.agreementService.agreements.update((list) => list.filter((a) => a.id !== id));
        } else {
          this.agreementService.getAgreements();
        }

        this.deleting.set(null);
        this.agreementToDelete.set(null);
        this.showDeleteModal.set(false);

        this.successMessage.set(`Agreement ${id} deleted`);
        setTimeout(() => this.successMessage.set(null), 3000);
      },
      error: (err) => {
        console.error('Error deleting agreement:', err);
        this.deleting.set(null);
        this.agreementToDelete.set(null);
        this.error.set(`Failed to delete agreement ${id}`);
      },
    });
  }
}
