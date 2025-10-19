import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SelectAuthorComponent } from './select-author.component';

describe('SelectUserComponent', () => {
  let component: SelectAuthorComponent;
  let fixture: ComponentFixture<SelectAuthorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SelectAuthorComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SelectAuthorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
