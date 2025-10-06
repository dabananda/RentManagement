import { Component, inject, OnInit, signal } from '@angular/core';
import { AgreementService } from '../../_services/agreement.service';
import { AgreementDetails as Agreement } from '../../_models/agreement';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-agreement-details',
  imports: [],
  templateUrl: './agreement-details.html',
  styleUrl: './agreement-details.css',
})
export class AgreementDetails implements OnInit {
  agreementService = inject(AgreementService);
  agreement = signal<Agreement | null>(null);
  private route = inject(ActivatedRoute);

  id: number = Number(this.route.snapshot.paramMap.get('id'));

  ngOnInit(): void {
    this.loadAgreement()
  }

  loadAgreement() {
    this.agreementService.getAgreement(this.id).subscribe({
      next: (agreement) => this.agreement.set(agreement),
      error: (err) => console.log('Error fetching agreement', err),
    });
  }
}
