import { Injectable, signal } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class TokenService {
  private _token = signal<string | null>(this.read());

  get token() {
    return this._token();
  }

  set(token: string | null) {
    this._token.set(token);
    if (token) {
      localStorage.setItem('token', token);
    } else {
      localStorage.removeItem('token');
    }
  }

  private read(): string | null {
    return localStorage.getItem('token');
  }
}
