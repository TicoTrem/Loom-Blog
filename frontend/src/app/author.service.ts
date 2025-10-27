import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';

export interface Author {
  id: number,
  name: string
}

export interface CreateAuthor {
  name: string
}

@Injectable({
  providedIn: 'root'
})
export class AuthorService {
  private http = inject(HttpClient)
  private apiUrl = 'http://localhost:5192/api/Author'

  get(id: number): Observable<Author> {
    return this.http.get<Author>(this.apiUrl + `/${id}`)
  }

  getAll(): Observable<Author[]> {
    return this.http.get<Author[]>(this.apiUrl)
  }

  create(author: CreateAuthor): Observable<Author> {
    return this.http.post<Author>(this.apiUrl, author)
  }

}
