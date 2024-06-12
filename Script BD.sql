--
-- Base de datos: `segpargmfdtexturas`
--
CREATE DATABASE `segpargmfdtexturas`;

USE `segpargmfdtexturas`;
--
-- Estructura de tabla para la tabla `texturas`
--

CREATE TABLE `texturas` (
  `id` int(11) NOT NULL,
  `descripcion` varchar(30) DEFAULT NULL,
  `cR` int(11) DEFAULT NULL,
  `cG` int(11) DEFAULT NULL,
  `cB` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `texturas`
--

INSERT INTO `texturas` (`id`, `descripcion`, `cR`, `cG`, `cB`) VALUES
(1, 'Tierra suelo', 90, 76, 61),
(2, 'piedra', 101, 101, 99),
(3, 'madera clara', 169, 144, 113),
(4, 'madera oscura', 155, 121, 84),
(5, 'marmol', 240, 233, 221),
(6, 'pared', 39, 73, 81),
(7, 'Pusheen', 180, 170, 160);

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `texturas`
--
ALTER TABLE `texturas`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `texturas`
--
ALTER TABLE `texturas`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;
COMMIT;
