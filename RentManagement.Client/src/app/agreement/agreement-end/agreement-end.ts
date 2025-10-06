import { Component, inject, OnInit, signal } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AgreementService } from '../../_services/agreement.service';
import { AgreementDetails } from '../../_models/agreement';

@Component({
  selector: 'app-agreement-end',
  imports: [],
  templateUrl: './agreement-end.html',
  styleUrl: './agreement-end.css',
})
export class AgreementEnd implements OnInit {
  private agreementService = inject(AgreementService);
  private router = inject(Router);
  private route = inject(ActivatedRoute);

  agreement = signal<AgreementDetails | null>(null);
  loading = signal<boolean>(false);
  error = signal<string | null>(null);
  agreementId: number = 0;

  ngOnInit(): void {
    this.agreementId = Number(this.route.snapshot.paramMap.get('id'));
    this.loadAgreement();
  }

  loadAgreement(): void {
    this.loading.set(true);
    this.agreementService.getAgreement(this.agreementId).subscribe({
      next: (agreement) => {
        this.agreement.set(agreement);
        this.loading.set(false);
      },
      error: (err) => {
        this.error.set(err.error?.Message || err.message || 'Failed to load agreement');
        this.loading.set(false);
      },
    });
  }

  confirmEnd(): void {
    if (!this.agreement()) return;

    this.loading.set(true);
    this.error.set(null);

    this.agreementService.endAgreement(this.agreementId).subscribe({
      next: () => {
        this.loading.set(false);
        this.router.navigate(['/agreements']);
      },
      error: (err) => {
        this.error.set(err.error?.Message || err.message || 'Failed to end agreement');
        this.loading.set(false);
      },
    });
  }

  cancel(): void {
    this.router.navigate(['/agreements']);
  }
}
