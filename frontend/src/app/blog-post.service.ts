import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Author } from './author.service';

export interface BlogPost {
  id: number;
  title: string;
  content: string;
  author: Author;
  createdDateUtc: string;
  lastUpdatedDateUtc: string;
}

export interface CreateBlogPost {
  title: string;
  content: string;
  authorId: number;
}

@Injectable({
  providedIn: 'root'
})
export class BlogPostService {

  private http = inject(HttpClient)
  private apiUrl = 'http://localhost:5192/blogpost'


  get(id: number): Observable<BlogPost> {
    return this.http.get<BlogPost>(this.apiUrl + `/${id}`)
  }

  getAll(): Observable<BlogPost[]> {
    return this.http.get<BlogPost[]>(this.apiUrl)
  }

  create(post: CreateBlogPost): Observable<BlogPost> {
    return this.http.post<BlogPost>(this.apiUrl, post)
  }
  constructor() { }
}
