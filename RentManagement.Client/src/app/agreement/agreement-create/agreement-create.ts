import { AgreementCreate as Agreement } from '../../_models/agreement';
import { Component, inject, OnInit, signal } from '@angular/core';
import { AgreementService } from '../../_services/agreement.service';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'app-agreement-create',
  imports: [ReactiveFormsModule],
  templateUrl: './agreement-create.html',
  styleUrl: './agreement-create.css',
})
export class AgreementCreate implements OnInit {
  private agreementService = inject(AgreementService);
  private router = inject(Router);
  private fb = inject(FormBuilder);

  formAgreement!: FormGroup;
  loading = signal<boolean>(false);
  error = signal<string | null>(null);

  ngOnInit(): void {
    this.initForm();
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

  onSubmit(): void {
    if (this.formAgreement.invalid) {
      this.formAgreement.markAllAsTouched();
      return;
    }

    this.loading.set(true);
    this.error.set(null);

    const formValue = this.formAgreement.value;
    const agreement: Agreement = {
      rentAmount: Number(formValue.rentAmount),
      securityFee: Number(formValue.securityFee),
      startDate: formValue.startDate,
      endDate: formValue.endDate || null,
      tenantId: Number(formValue.tenantId),
      shopId: Number(formValue.shopId),
    };

    this.agreementService.createAgreement(agreement).subscribe({
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
