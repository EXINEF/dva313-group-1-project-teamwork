from django.urls import path
from .views import homePage, indexPage

urlpatterns = [
    path('', indexPage, name='index'),
    path('home', homePage, name='home'),
]