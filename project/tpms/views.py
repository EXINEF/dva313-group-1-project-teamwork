from django.shortcuts import redirect, render
from django.contrib.auth.forms import AuthenticationForm
from .models import Vehicle, Tire, Sensor

# Create your views here.
def indexPage(request):
    form = AuthenticationForm()

    if request.method == "POST":
        return redirect('home')

    context = {'form':form}
    return render(request,'index/index.html', context)

def homePage(request):
    vehicles = Vehicle.objects.all()

    context = {'vehicles':vehicles}
    return render(request, 'user/home.html', context)

def vehiclePage(request, pk):
    vehicle = Vehicle.objects.get(id=pk)
    tires = vehicle.tires.all()

    context = {'vehicle':vehicle, 'tires':tires}
    return render(request, 'user/vehicle.html', context)

def tirePage(request, pk):
    tire = Tire.objects.get(id=pk)

    context = {'tire':tire}
    return render(request, 'user/tire.html', context)

def sensorPage(request, pk):
    sensor = Sensor.objects.get(id=pk)

    context = {'sensor':sensor}
    return render(request, 'user/sensor.html', context)

# https://tmps-volvo.herokuapp.com/send-data-VEHICLE_ID-SENSOR_ID-20-20-SENSOR_ID-20-20-SENSOR_ID-20-20-SENSOR_ID-20-20
def sendData(request, query):
    return 