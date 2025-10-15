import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EasymdeEditorComponent } from './easymde-editor.component';

describe('EasymdeEditorComponent', () => {
  let component: EasymdeEditorComponent;
  let fixture: ComponentFixture<EasymdeEditorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EasymdeEditorComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EasymdeEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
