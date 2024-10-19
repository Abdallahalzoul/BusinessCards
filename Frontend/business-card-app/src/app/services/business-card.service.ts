import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment.development';

@Injectable({
  providedIn: 'root',
})
export class BusinessCardService {
  private apiUrl = environment.Api_URL;

  constructor(private http: HttpClient) {}

  getBusinessCards(): Observable<any> {
    return this.http.post(`${this.apiUrl}/list`, {});
  }

  getBusinessCardById(id: number): Observable<any> {
    return this.http.post(`${this.apiUrl}/get`, { id });
  }

  addBusinessCard(businessCard: any): Observable<any> {

    return this.http.post(`${this.apiUrl}/create`, businessCard);
  }

  deleteBusinessCard(id: number): Observable<any> {
    return this.http.post(`${this.apiUrl}/delete`, { id });
  }

  filterBusinessCards(filter: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/filter`, filter);
  }

  exportBusinessCards(format: string): Observable<any> {
    const body = { format };
    return this.http.post(`${this.apiUrl}/export`, body, {
      responseType: 'blob',
      observe: 'response',
    });
  }

  importBusinessCards(file: File): Observable<any> {
    const formData = new FormData();
    formData.append('File', file);
    return this.http.post(`${this.apiUrl}/import`, formData);
  }
}
