from django.shortcuts import redirect, render
from django.contrib.auth.forms import AuthenticationForm
from .models import Vehicle

# Create your views here.
def indexPage(request):
    form = AuthenticationForm()

    if request.method == "POST":
        return redirect('home')

    context = {'form':form}
    return render(request,'index/index.html', context)

def homePage(request):
    vehicles = Vehicle.objects.all()
    for vehicle in vehicles:
        print(vehicle.id, vehicle.model)
        for tire in vehicle.tires.all():
            print(tire.id)
        print('\n')
    context = {'vehicles':vehicles}
    return render(request, 'user/home.html', context)