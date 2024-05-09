import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class AdminSharedDataService {
  constructor() {}

  setSession = (key: string, value: any) => {
    // stringify = converting object to string
    sessionStorage.setItem(key, JSON.stringify(value));
  };

  getSession = (key: string): any => {
    const item = sessionStorage.getItem(key);
    // parse = converting string to object
    return item ? JSON.parse(item) : null;
  };

  clearSessionData = (key: string) => {
    sessionStorage.removeItem(key);
  };
}
