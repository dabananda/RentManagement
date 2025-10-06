import { Component, inject, OnInit, signal } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AgreementService } from '../../_services/agreement.service';
import { AgreementCreate as Agreement } from '../../_models/agreement';

@Component({
  selector: 'app-agreement-update',
  imports: [ReactiveFormsModule],
  templateUrl: './agreement-update.html',
  styleUrl: './agreement-update.css',
})
export class AgreementUpdate implements OnInit {
  private agreementService = inject(AgreementService);
  private router = inject(Router);
  private route = inject(ActivatedRoute);
  private fb = inject(FormBuilder);

  formAgreement!: FormGroup;
  loading = signal<boolean>(false);
  error = signal<string | null>(null);
  agreementId: number = 0;

  ngOnInit(): void {
    this.agreementId = Number(this.route.snapshot.paramMap.get('id'));
    this.initForm();
    this.loadAgreement();
  }

  initForm(): void {
    this.formAgreement = this.fb.group({
      rentAmount: ['', Validators.required],
      securityFee: ['', Validators.required],
      startDate: ['', Validators.required],
      endDate: [''],
      tenantId: ['', Validators.required],
      shopId: ['', Validators.required],
    });
  }

  loadAgreement(): void {
    this.loading.set(true);

    // Use the existing getAgreement to work with cached or fetched details
    this.agreementService.getAgreement(this.agreementId).subscribe({
      next: (details: any) => {
        // Handle both shapes:
        // 1) Flat:  { tenantId, shopId }
        // 2) Nested:{ tenant: {id}, shop: {id} }
        const tenantId = details?.tenantId ?? details?.tenant?.id ?? '';
        const shopId = details?.shopId ?? details?.shop?.id ?? '';

        // Normalize dates for <input type="date"> controls (YYYY-MM-DD)
        const toDateInput = (v: string | null | undefined) => (v ?? '').toString().slice(0, 10);

        this.formAgreement.patchValue({
          rentAmount: details?.rentAmount ?? '',
          securityFee: details?.securityFee ?? '',
          startDate: toDateInput(details?.startDate),
          endDate: toDateInput(details?.endDate),
          tenantId,
          shopId,
        });

        this.loading.set(false);
      },
      error: (err) => {
        this.error.set(err.error?.Message || err.message || 'Failed to load agreement');
        this.loading.set(false);
      },
    });
  }

  onSubmit(): void {
    if (this.formAgreement.invalid) {
      this.formAgreement.markAllAsTouched();
      return;
    }

    this.loading.set(true);
    this.error.set(null);

    const v = this.formAgreement.value;
    const agreement: Agreement = {
      rentAmount: Number(v.rentAmount),
      securityFee: Number(v.securityFee),
      startDate: v.startDate,
      endDate: v.endDate || null,
      tenantId: Number(v.tenantId),
      shopId: Number(v.shopId),
    };

    this.agreementService.updateAgreement(this.agreementId, agreement).subscribe({
      next: () => {
        this.loading.set(false);
        this.router.navigate(['/agreements']);
      },
      error: (err) => {
        this.error.set(err.error?.Message || err.message || 'An error occurred');
        this.loading.set(false);
      },
    });
  }

  cancel(): void {
    this.router.navigate(['/agreements']);
  }
}
