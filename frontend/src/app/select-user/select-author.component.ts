import { Component } from '@angular/core';
import { Author, AuthorService } from '../author.service';

@Component({
  selector: 'app-select-user',
  imports: [],
  templateUrl: './select-author.component.html',
  styleUrl: './select-author.component.css'
})
export class SelectAuthorComponent {

  authors: Author[] | null = null;

  constructor(private authorService: AuthorService){}

  ngOnInit() {
    this.authorService.getAll().subscribe({ next: (data) => this.authors = data })
  }
}
