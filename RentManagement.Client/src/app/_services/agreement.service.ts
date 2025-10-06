import { HttpClient } from '@angular/common/http';
import { effect, inject, Injectable, signal } from '@angular/core';
import { AgreementCreate, AgreementDetails } from '../_models/agreement';
import { environment } from '../../environments/environment';
import { of } from 'rxjs';
import { Account } from './account';

@Injectable({
  providedIn: 'root',
})
export class AgreementService {
  private http = inject(HttpClient);
  private account = inject(Account);
  private baseUrl = environment.apiUrl;

  agreements = signal<AgreementDetails[]>([]);

  constructor() {
    effect(() => {
      const user = this.account.currentUser();
      this.agreements.set([]);
      if (user) this.getAgreements();
    });
  }

  getAgreements() {
    return this.http.get<AgreementDetails[]>(`${this.baseUrl}/RentalAgreement`).subscribe({
      next: (agreement) => this.agreements.set(agreement),
      error: (err) => console.log('Error fetching agreements', err),
    });
  }

  getAgreement(id: number) {
    const agreement = this.agreements().find((x) => x.id === id);
    if (agreement !== undefined) return of(agreement);
    return this.http.get<AgreementDetails>(`${this.baseUrl}/RentalAgreement/${id}`);
  }

  createAgreement(agreement: AgreementCreate) {
    return this.http.post<AgreementCreate>(`${this.baseUrl}/RentalAgreement`, agreement);
  }

  getAgreementForUpdate(id: number) {
    return this.http.get<AgreementCreate>(`${this.baseUrl}/RentalAgreement/${id}`);
  }

  updateAgreement(id: number, agreement: AgreementCreate) {
    return this.http.put<AgreementCreate>(`${this.baseUrl}/RentalAgreement/${id}`, agreement);
  }

  endAgreement(id: number) {
    return this.http.post(`${this.baseUrl}/RentalAgreement/EndAgreement/${id}`, {});
  }

  deleteAgreement(id: number) {
    return this.http.delete(`${this.baseUrl}/RentalAgreement/${id}`);
  }
}
