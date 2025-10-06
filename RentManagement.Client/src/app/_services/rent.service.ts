import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { Rent, RentCreate } from '../_models/rent';
import { environment } from '../../environments/environment';
import { of } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class RentService {
  private http = inject(HttpClient);
  private baseUrl = environment.apiUrl;

  rents = signal<Rent[]>([]);

  getRents() {
    return this.http.get<Rent[]>(`${this.baseUrl}/RentRecord`).subscribe({
      next: (rents) => this.rents.set(rents),
      error: (err) => console.log('Error fetching rents', err),
    });
  }

  getRent(id: number) {
    const rent = this.rents().find((r) => r.id === id);
    if (rent !== undefined) return of(rent);
    return this.http.get<Rent>(`${this.baseUrl}/RentRecord/${id}`);
  }

  addRent(rent: RentCreate) {
    return this.http.post<RentCreate>(`${this.baseUrl}/RentRecord`, rent);
  }

  deleteRent(id: number) {
    return this.http.delete(`${this.baseUrl}/RentRecord/${id}`);
  }
}
