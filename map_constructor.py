# MCC - Map Collision Constructor
# Программа для построения карт коллизий для игры Hero Return

# Алгоритм работы:
# 1. Вставить в константу IMG_PATH путь до карты
# 2. Запустить программу
# 3. Q - вкл./выкл. сетки
# 4. E - вкл./выкл. режима редактирования
# 5. F - вкл./выкл. значений клеток (Работает только в режиме редактрования)
# (Красные - 100, Синие - 1-10)
# 6. SHIFT+UP - увеличить значение клетки в режиме редактирования
# 7. SHIFT+DOWN - уменьшить значение клетки в режиме редактирования
# 8. CTRL+UP - максимальное значение клетки в режиме редактирования
# 9. CTRL+DOWN - минимальное значение клетки в режиме редактирования
# 10. UP, DOWN, LEFT, RIGHT - передвижение по карте
# 11. S - сохранить карту коллайдеров в файл "Data/map_colliders.json"
# 12. L - загрузить карту коллайдеров из файла "Data/map_colliders.json"
# 13. ESC - выход из программы

import pygame
import json

WIDTH = 27
HEIGHT = 15
CELL_SIZE = 48
IMG_PATH = ""


class Map:
    """Класс для описания объекта карты. Содержит объект изображения, ширину, высоту и размер одной клетки."""
    def __init__(self, image, cell_size=48):
        self.__image = image
        self.__width = image.get_width()//cell_size  # измеряется в клетках
        self.__height = image.get_height()//cell_size  # измеряется в клетках
        self.__cell_size = cell_size

    @property
    def image(self):
        return self.__image

    @property
    def cell_size(self):
        return self.__cell_size

    @property
    def width(self):
        return self.__width

    @width.setter
    def width(self, new_width):
        self.__width = new_width

    @property
    def height(self):
        return self.__height

    @height.setter
    def height(self, new_height):
        self.__height = new_height


class Camera(object):
    """Класс для описания объекта камеры. Содержит её позицию, размер экрана и объект окна приложения.

        Атрибуты:
            x:int - X-координата камеры в клетках

            y:int - Y-координата камеры в клетках

            width:int - Ширина экрана в клетках

            height:int - Высоту экрана в клетках

            grid:bool - Включена ли сетка

            edit:bool - Включен ли режим редактирования

            is_draw_numbers:bool - Включено ли отображение цифр в режиме редактирования
        Методы:
            toggle_grid() - Переклюает отображение сетки

            toggle_edit() - Переключает режим редактирования

            toggle_draw_numbers() - Переключает отображение цифр в режиме редактирование

            set_position(x, y, map_object) - Устанавливает позицию камеры, не позволяя выйти за пределы карты
    """
    def __init__(self, x=0, y=0, display_width=27, display_height=15, cell_size=48):
        self.__x = x  # in cells
        self.__y = y
        self.__width = display_width  # in cells
        self.__height = display_height
        display = (display_width*cell_size, display_height*cell_size)
        self.__grid = False
        self.__edit = False
        self.__is_draw_numbers = True
        self.__display = pygame.display.set_mode(display, pygame.SCALED | pygame.DOUBLEBUF, 32)

    @property
    def x(self):
        """Возвращает X-координату камеры в клетках"""
        return self.__x

    @property
    def y(self):
        """Возвращает Y-координату камеры в клетках"""
        return self.__y

    @property
    def width(self):
        """Возвращает ширину экрана в клетках"""
        return self.__width

    @property
    def height(self):
        """Возвращает высоту экрана в клетках"""
        return self.__height

    @property
    def grid(self):
        return self.__grid

    def toggle_grid(self):
        """Переклюает отображение сетки."""
        self.__grid = not self.__grid

    @property
    def edit(self):
        return self.__edit

    def toggle_edit(self):
        """Переключает режим редактирования."""
        self.__edit = not self.__edit

    @property
    def is_draw_numbers(self):
        return self.__is_draw_numbers

    def toggle_draw_numbers(self):
        """Переключает отображение цифр в режиме редактирование."""
        self.__is_draw_numbers = not self.__is_draw_numbers

    @property
    def display(self):
        return self.__display

    def set_position(self, x, y, map_object):
        """Устанавливает позицию камеры, не позволяя выйти за пределы карты."""
        if x >= 0 and x+self.width <= map_object.width:
            self.__x = x
        if y >= 0 and y+self.height <= map_object.height:
            self.__y = y


class Colliders:
    """Класс для описания карты коллайдеров. Содержит её позицию, размер экрана и объект окна приложения.

        Атрибуты:
            colliders:list - матрица карты коллайдеров

            edit_value:int - значение изменения состояния клетки в режиме редактирования
        Методы:
            fill_colliders(map_object) - Заполняет матрица коллайдеров нулями по размеру карты.

            set_collider_value(x, y) - Устанавливает значение клетки по координатам в матрице

            load(file_path) - Загружает матрицу карты коллайдеров из файла

            save(file_path) - Сохраняет матрицу карты коллайдеров в файл
    """
    def __init__(self):
        self.__colliders = []
        self.__edit_value = 1

    def fill_colliders(self, map_object):
        """Заполняет матрицу коллайдеров '0' по размеру карты."""
        for w in range(map_object.width):
            self.__colliders.append([])
            for h in range(map_object.height):
                self.__colliders[w].append(0)

    def set_collider_value(self, x, y):
        self.__colliders[x][y] = self.__edit_value

    @property
    def colliders(self):
        return self.__colliders

    @property
    def edit_value(self):
        return self.__edit_value

    @edit_value.setter
    def edit_value(self, new_value):
        self.__edit_value = new_value

    def load(self, file_path):
        """Загружает карту коллайдеров из файла."""
        try:
            with open(file_path) as colliders_file:
                self.__colliders = json.load(colliders_file)
        except FileNotFoundError:
            print("Сохранённого файла карты коллайдеров не существует.")

    def save(self, file_path):
        """Сохраняет карту коллайдеров в файл."""
        with open(file_path, "w") as colliders_file:
            json.dump(self.__colliders, colliders_file)


def draw_rect_alpha(surface, color, rect):
    """ Рисует прямоугольник с прохрачностью"""
    shape_surf = pygame.Surface(pygame.Rect(rect).size, pygame.SRCALPHA).convert_alpha()
    pygame.draw.rect(shape_surf, color, shape_surf.get_rect())
    surface.blit(shape_surf, rect)


def draw(camera_object, map_object, collider_object):
    """Функция отрисовки объектов"""
    camera_object.display.fill((0, 0, 0))
    camera_object.display.blit(map_object.image,
                               ((-1)*camera_object.x*map_object.cell_size,
                                (-1)*camera_object.y*map_object.cell_size))

    if camera_object.grid:
        for w in range(camera_object.width):
            for h in range(camera_object.height):
                pygame.draw.line(camera_object.display, (255, 255, 255), (48*w, 0+48*h), (48*w+5, 48*h))
                pygame.draw.line(camera_object.display, (255, 255, 255), (43+48*w, 48*h), (48+48*w+5, 0+48*h))
                pygame.draw.line(camera_object.display, (255, 255, 255), (48*w, 48*h), (48*w, 48*h+5))
                pygame.draw.line(camera_object.display, (255, 255, 255), (48*w, 43+48*h), (48*w, 48+48*h+5))

    if camera_object.edit:
        for w in range(camera_object.display.get_width()//48):
            for h in range(camera_object.display.get_height()//48):
                if collider_object.colliders[w+camera_object.x][h+camera_object.y] != 0:
                    if collider_object.colliders[w+camera_object.x][h+camera_object.y] == 100:
                        draw_rect_alpha(camera_object.display,
                                        (255, 0, 0, 120),
                                        (w * map_object.cell_size,
                                         h * map_object.cell_size,
                                         48, 48))
                    else:
                        draw_rect_alpha(camera_object.display,
                                        (117, 193, 255, 120),
                                        (w*map_object.cell_size,
                                         h*map_object.cell_size,
                                         48, 48))
                        if camera.is_draw_numbers:
                            font = pygame.font.SysFont("segoeui", 14, True)
                            text = font.render(str(collider_object.colliders[w+camera_object.x][h+camera_object.y]),
                                               True, (255, 255, 255)).convert_alpha()
                            camera_object.display.blit(text, (w*map_object.cell_size+3,
                                                              h*map_object.cell_size+3))

        mouse_x = pygame.mouse.get_pos()[0] // 48
        mouse_y = pygame.mouse.get_pos()[1] // 48
        draw_rect_alpha(camera.display, (153, 153, 153, 120), (mouse_x * 48, mouse_y * 48, 48, 48))

    info = f'Сетка(Q) - {"вкл." if camera_object.grid else "выкл."}              ' \
           f'Редактирование(E) - {"вкл." if camera_object.edit else "выкл."}              ' \
           f'Цифры(F) - {"вкл." if camera_object.is_draw_numbers else "выкл."}              ' \
           f'Значение - {collider_object.edit_value}'
    text = pygame.font.SysFont("segoeui", 14, True).render(info, True, (255, 255, 255), (0, 0, 0))
    rect = text.get_rect()
    camera.display.blit(text, (camera_object.width*48-rect.width, 720-rect.height))


if __name__ == "__main__":
    pygame.init()
    timer = pygame.time.Clock()

    camera = Camera(0, 0)
    collider = Colliders()
    main_image = pygame.image.load(IMG_PATH).convert()
    main_map = Map(main_image)
    pygame.display.set_caption("MCC - Map Collision Constructor")

    collider.fill_colliders(main_map)

    running = True
    while running:
        timer.tick(60)
        for e in pygame.event.get():
            if e.type == pygame.QUIT:
                running = False
            if e.type == pygame.KEYDOWN:
                if e.mod == pygame.KMOD_NONE:
                    if e.key == pygame.K_ESCAPE:
                        running = False
                    if e.key == pygame.K_DOWN:
                        camera.set_position(camera.x, camera.y+1, main_map)
                    if e.key == pygame.K_UP:
                        camera.set_position(camera.x, camera.y-1, main_map)
                    if e.key == pygame.K_RIGHT:
                        camera.set_position(camera.x+1, camera.y, main_map)
                    if e.key == pygame.K_LEFT:
                        camera.set_position(camera.x-1, camera.y, main_map)
                    if e.key == pygame.K_q:
                        camera.toggle_grid()
                    if e.key == pygame.K_e:
                        camera.toggle_edit()
                    if e.key == pygame.K_s:
                        collider.save("Data/map_colliders.json")
                    if e.key == pygame.K_l:
                        collider.load("Data/map_colliders.json")
                    if e.key == pygame.K_f:
                        camera.toggle_draw_numbers()

                if e.mod == pygame.KMOD_LSHIFT:
                    if e.key == pygame.K_UP:
                        if collider.edit_value < 10:
                            collider.edit_value += 1
                        else:
                            collider.edit_value = 100
                    if e.key == pygame.K_DOWN:
                        if 0 < collider.edit_value <= 10:
                            collider.edit_value -= 1
                        elif collider.edit_value > 0:
                            collider.edit_value = 10
                if e.mod == pygame.KMOD_LCTRL:
                    if e.key == pygame.K_UP:
                        collider.edit_value = 100
                    if e.key == pygame.K_DOWN:
                        collider.edit_value = 0
            if e.type == pygame.MOUSEBUTTONDOWN and camera.edit:
                collider.set_collider_value((e.pos[0]//48)+camera.x, (e.pos[1]//48)+camera.y)

        draw(camera, main_map, collider)
        pygame.display.update()
