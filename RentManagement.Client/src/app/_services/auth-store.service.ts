import { Injectable, signal } from '@angular/core';
import { AuthState } from '../_models/auth-state';

const KEY = 'auth';

@Injectable({
  providedIn: 'root',
})
export class AuthStoreService {
  private _state = signal<AuthState | null>(this.read());

  get state() {
    return this._state();
  }

  get token() {
    return this._state()?.token ?? null;
  }

  set(state: AuthState) {
    localStorage.setItem(KEY, JSON.stringify(state));
    this._state.set(state);
  }

  clear() {
    localStorage.removeItem(KEY);
    this._state.set(null);
  }

  private read(): AuthState | null {
    const raw = localStorage.getItem(KEY);
    if (raw) return JSON.parse(raw) as AuthState;
    return null;
  }
}
