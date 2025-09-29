import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';

export interface BlogPost {
  content: string;
  author: Author;
}
export interface Author {
  name: string;
}

@Injectable({
  providedIn: 'root'
})
export class BlogPostService {

  private http = inject(HttpClient)
  private apiUrl = 'http://localhost:5192/blogpost'


  getAll(): Observable<BlogPost[]> {
    return this.http.get<BlogPost[]>(this.apiUrl)
  }
  constructor() { }
}
