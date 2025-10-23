import { inject, Injectable, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { CardData } from '../_models/card-data';
import { AgreementTable } from '../_models/agreement-table';

@Injectable({
  providedIn: 'root',
})
export class DashboardService {
  private http = inject(HttpClient);
  private baseUrl = environment.apiUrl;

  loadCardData() {
    return this.http.get<CardData>(`${this.baseUrl}/dashboard`);
  }

  loadAgreementTable() {
    return this.http.get<AgreementTable[]>(`${this.baseUrl}/dashboard/table`);
  }
}
